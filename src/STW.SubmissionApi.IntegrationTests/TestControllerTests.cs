namespace STW.SubmissionApi.IntegrationTests;

using System.Net;
using System.Net.Mime;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TestControllerTests
{
    private const string RequestUri = "/test";
    private readonly HttpClient _httpClient;

    public TestControllerTests()
    {
        var factory = new SubmissionApiWebApplicationFactory();
        _httpClient = factory.CreateClient();
    }

    [TestMethod]
    public async Task TestControllerPost_ReturnsRequestBodyAndContentTypeInResponse_WhenCalled()
    {
        // Act
        const string requestBody = "Some string";
        var response = await _httpClient.PostAsync(RequestUri, new StringContent(requestBody));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Text.Plain);

        var responseBody = await response.Content.ReadAsStringAsync();
        responseBody.Should().Be(requestBody);
    }
}
