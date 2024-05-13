namespace STW.SubmissionApi.Presentation.UnitTests.Controllers;

using System.Net.Mime;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.Controllers;

[TestClass]
public class TestControllerTests
{
    private TestController _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new TestController();
    }

    [TestMethod]
    public async Task SamplePost_ReturnsRequestBodyAndContentType_WhenCalled()
    {
        // Arrange
        const string requestBody = "{ \"key\": \"value\" }";

        _systemUnderTest.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                Request =
                {
                    Headers =
                    {
                        { HeaderNames.ContentType, MediaTypeNames.Application.Json }
                    },
                    Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody))
                }
            }
        };

        // Act
        var result = await _systemUnderTest.SamplePost() as ContentResult;

        // Assert
        result.StatusCode.Should().Be(StatusCodes.Status200OK);
        result.ContentType.Should().Be(MediaTypeNames.Application.Json);
        result.Content.Should().Be(requestBody);
    }
}
