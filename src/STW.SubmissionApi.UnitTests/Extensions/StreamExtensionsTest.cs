using System.Text;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STW.SubmissionApi.Extensions;

namespace STW.SubmissionApi.UnitTests.Extensions;

[TestClass]
[TestSubject(typeof(StreamExtensions))]
public class StreamExtensionsTest
{
    [TestMethod]
    public async Task ReadAsStringAsync_ReturnsStringFromStream()
    {
        const string content = "Content";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var result = await stream.ReadAsStringAsync();

        result.Should().Be(content);
    }
}
