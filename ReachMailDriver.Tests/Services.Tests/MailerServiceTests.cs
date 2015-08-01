using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReachMailDriver.Services;
using ReachMailDriver.Services.Gateway;
using System;

namespace ReachMailDriver.Tests.Services.Tests
{
    [TestClass]
    public class MailerServiceTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateMailer_NullArguments_ThrowsException()
        {
            // Arrange
            var mockApiGateway = new Mock<IReachMailApiGateway>();
            var mockMailerService = new MailerService(mockApiGateway.Object);

            // Act & Assert 
            mockMailerService.CreateMailer("", "fromEmail", "fromName", "replyToEmail", "subject", "text");
        }

        [TestMethod]
        public void CreateMailer_ValidArgs_ReturnsGuid()
        {
            // Arrange
            var knownGuid = Guid.NewGuid();
            var mockApiGateway = new Mock<IReachMailApiGateway>();
            mockApiGateway.Setup(m => m.CreateMailer(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()))
                .Returns(knownGuid);

            var mailerService = new MailerService(mockApiGateway.Object);

            // Act
            var list = mailerService.CreateMailer("name", "fromEmail", "fromName", "replyToEmail", "subject", "text");

            // Assert
            Assert.AreEqual(knownGuid, list.Id);
        }
    }
}
