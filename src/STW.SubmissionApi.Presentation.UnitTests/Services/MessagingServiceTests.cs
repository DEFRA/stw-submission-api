namespace STW.SubmissionApi.Presentation.UnitTests.Services;

using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Options;
using Presentation.Services;

[TestClass]
public class MessagingServiceTests
{
    private MessagingService _systemUnderTest;
    private Mock<ServiceBusClient> _serviceBusClientMock;
    private Mock<ServiceBusSender> _serviceBusSenderMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _serviceBusClientMock = new Mock<ServiceBusClient>();
        _serviceBusSenderMock = new Mock<ServiceBusSender>();
        _serviceBusClientMock.Setup(serviceBusClient => serviceBusClient.CreateSender(It.IsAny<string>()))
            .Returns(_serviceBusSenderMock.Object);
        _systemUnderTest = new MessagingService(
            _serviceBusClientMock.Object, Options.Create(new ServiceBusOptions()));
    }

    [TestMethod]
    public async Task SendMessageAsync_SendMessageViaServiceBusSender_WhenCalled()
    {
        // Arrange
        const string message = "some message";

        // Act
        await _systemUnderTest.SendMessageAsync(message);

        // Assert
        _serviceBusSenderMock.Verify(
            sender => sender.SendMessageAsync(
                It.Is<ServiceBusMessage>(
                    serviceBusMessage => serviceBusMessage.Body.ToString() == message),
                CancellationToken.None),
            Times.Once);
    }
}
