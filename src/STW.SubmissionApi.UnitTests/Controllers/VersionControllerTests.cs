using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STW.SubmissionApi.Controllers;

namespace STW.SubmissionApi.UnitTests.Controllers;

[TestClass]
public class VersionControllerTests
{
    private VersionController _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _systemUnderTest = new VersionController();
    }

    [TestMethod]
    public void Sample()
    {
        // Act
        var result = _systemUnderTest.Index();

        // Assert
        result.Should().Be("Running on Version 1.0");
    }
}