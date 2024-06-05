namespace STW.SubmissionApi.Presentation.UnitTests.Services;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Newtonsoft.Json;
using NJsonSchema.Validation;
using Presentation.Services;

[TestClass]
public class ValidationServiceTests
{
    private ValidationService _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new ValidationService();
    }

    [TestMethod]
    public async Task Validate_ReturnsEmptyList_WhenValidationPasses()
    {
        // Arrange
        var spsCertificate = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");

        // Act
        var result = await _systemUnderTest.Validate(spsCertificate);

        // Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public async Task Validate_ReturnsListOfErrors_WhenValidationFails()
    {
        // Arrange
        var spsCertificateString = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(spsCertificateString);
        spsCertificate!.SpsExchangedDocument.StatusCode.Value = "1";
        spsCertificate.SpsConsignment.MainCarriageSpsTransportMovement.First().Id.Value = string.Empty;
        spsCertificateString = JsonConvert.SerializeObject(spsCertificate);

        // Act
        var result = await _systemUnderTest.Validate(spsCertificateString);

        // Assert
        result.Should().SatisfyRespectively(
            error =>
            {
                error.Path.Should().Be("#/spsConsignment.mainCarriageSpsTransportMovement[0].id.value");
                error.Kind.Should().Be(ValidationErrorKind.StringTooShort);
            },
            error =>
            {
                error.Path.Should().Be("#/spsExchangedDocument.statusCode.value");
                error.Kind.Should().Be(ValidationErrorKind.NotInEnumeration);
            });
    }
}
