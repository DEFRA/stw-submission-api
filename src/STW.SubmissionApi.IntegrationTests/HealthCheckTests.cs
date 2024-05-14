namespace STW.SubmissionApi.IntegrationTests;

using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.HealthChecks;

[TestClass]
public class HealthCheckTests
{
    private const string RequestUri = "/health";
    private readonly HttpClient _httpClient;

    public HealthCheckTests()
    {
        var factory = new SubmissionApiWebApplicationFactory();
        _httpClient = factory.CreateClient();
    }

    [TestMethod]
    public async Task SamplePost_ReturnsOkWithRequestBody_WhenCalled()
    {
        // Act
        var response = await _httpClient.GetAsync(RequestUri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseBody = await response.Content.ReadFromJsonAsync<HealthCheckResponse>();
        responseBody?.Status.Should().Be("Healthy");
        responseBody?.Version.Should().Be("Unknown");
    }
}
