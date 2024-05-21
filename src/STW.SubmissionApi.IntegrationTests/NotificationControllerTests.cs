namespace STW.SubmissionApi.IntegrationTests;

using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Newtonsoft.Json;
using Presentation.Models;

[TestClass]
public class NotificationControllerTests
{
    private const string RequestUri = "/notification";
    private static HttpClient _client;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        var factory = new SubmissionApiWebApplicationFactory();
        _client = factory.CreateClient();
    }

    [TestMethod]
    public async Task Post_ReturnsAcceptedResponse_WhenValidationPasses()
    {
        // Arrange
        var requestBody = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var content = new StringContent(requestBody);

        // Act
        var response = await _client.PostAsync(RequestUri, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        var body = await response.Content.ReadFromJsonAsync<SubmissionAcceptedDto>();

        body.Should().NotBeNull();
        body?.Id.Should().NotBeEmpty();
    }

    [TestMethod]
    public async Task Post_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        var requestBodyString = await File.ReadAllTextAsync("TestData/minimalSpsCertificate.json");
        var requestBody = JsonConvert.DeserializeObject<SpsCertificate>(requestBodyString);
        requestBody!.SpsExchangedDocument.StatusCode.Value = "1";
        var content = new StringContent(JsonConvert.SerializeObject(requestBody));

        // Act
        var response = await _client.PostAsync(RequestUri, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        body.Should().NotBeNull();
        body?.Type.Should().Be("https://tools.ietf.org/html/rfc9110#section-15.5.1");
        body?.Title.Should().Be("One or more validation errors occurred.");
        body?.Status.Should().Be(400);
        body?.Errors.Should().HaveCount(1)
            .And.ContainKey("#/spsExchangedDocument.statusCode.value")
            .WhoseValue.Should().Equal("NotInEnumeration: #/spsExchangedDocument.statusCode.value");
    }
}
