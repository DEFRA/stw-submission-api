using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NJsonSchema.Validation;
using STW.SubmissionApi.Models;
using STW.SubmissionApi.Services;

namespace STW.SubmissionApi.UnitTests.Services;

[TestClass]
[TestSubject(typeof(ValidationService))]
public class ValidationServiceTest
{
    private readonly ValidationService _systemUnderTest = new();

    [TestMethod]
    public async Task Validate_ReturnsEmptyList_WhenValidationPasses()
    {
        var spsCertificate = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");

        var result = await _systemUnderTest.Validate(spsCertificate);

        result.Should().BeEmpty();
    }

    [TestMethod]
    public async Task Validate_ReturnsListOfErrors_WhenValidationFails()
    {
        var spsCertificateString = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var spsCertificate = JsonConvert.DeserializeObject<SpsCertificate>(spsCertificateString);
        spsCertificate!.SpsExchangedDocument.StatusCode.Value = "1";
        spsCertificate.SpsConsignment.MainCarriageSpsTransportMovement.First().Id.Value = string.Empty;
        spsCertificateString = JsonConvert.SerializeObject(spsCertificate);

        var result = await _systemUnderTest.Validate(spsCertificateString);

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
