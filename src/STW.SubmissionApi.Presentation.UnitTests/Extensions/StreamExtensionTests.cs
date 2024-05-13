namespace STW.SubmissionApi.Presentation.UnitTests.Extensions;

using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.Extensions;

[TestClass]
public class StreamExtensionTests
{
    [TestMethod]
    public async Task ReadAsStringAsync_ReturnsContentAsString_WhenCalled()
    {
        // Arrange
        const string content = "some content";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        // Act
        var result = await stream.ReadAsStringAsync();

        // Assert
        result.Should().Be(content);
    }
}
