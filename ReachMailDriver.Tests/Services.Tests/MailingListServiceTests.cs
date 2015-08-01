using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReachMailDriver.Services;
using ReachMailDriver.Services.Gateway;
using System;
using System.Collections.Generic;

namespace ReachMailDriver.Tests.Services.Tests
{
    [TestClass]
    public class MailingListServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MailingListService_NoName_ThrowsException()
        {
            // Arrange
            var mockApiGateway = new Mock<IReachMailApiGateway>();
            var mockMailingListService = new MailingListService(mockApiGateway.Object);

            // Act & Assert 
            mockMailingListService.CreateMailingList("");
        }

        [TestMethod]
        public void MailingListService_ValidArgs_CreatesMailingList()
        {
            // Arrange
            var knownGuid = Guid.NewGuid();
            var mockApiGateway = new Mock<IReachMailApiGateway>();
            var mockMailingListService = new MailingListService(mockApiGateway.Object);
            mockApiGateway.Setup(m => m.CreateMailingList(It.IsAny<String>()))
                .Returns(knownGuid);

            // Act
            var list = mockMailingListService.CreateMailingList("listName");

            // Assert
            Assert.AreEqual(knownGuid, list.Id);
        }

        public void MailingListService_Recipients_CreatesMailingList()
        {
            // Arrange
            var recipients = new List<String> { "email1@email.com", "email2@email.com" };
            var knownGuid = Guid.NewGuid();
            var mockApiGateway = new Mock<IReachMailApiGateway>();
            var mockMailingListService = new MailingListService(mockApiGateway.Object);
            mockApiGateway.Setup(m => m.CreateMailingList(It.IsAny<String>()))
                .Returns(knownGuid);

            // Act
            var list = mockMailingListService.CreateMailingList("listName", recipients);

            // Assert
            Assert.AreEqual(knownGuid, list.Id);
            Assert.AreEqual(2, list.RecipientEmails.Count);
        }
    }
}
