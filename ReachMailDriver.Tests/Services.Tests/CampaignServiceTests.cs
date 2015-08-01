using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReachMailDriver.Services;
using ReachMailDriver.Services.Gateway;
using System;

namespace ReachMailDriver.Tests.Services.Tests
{
    [TestClass]
    public class CampaignServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ScheduleMailingCampaing_NullArguments_ThrowsException()
        {
            // Arrange
            var mockApiGateway = new Mock<IReachMailApiGateway>();
            var mockCampaignService = new CampaignService(mockApiGateway.Object);

            // Act & Assert
            mockCampaignService.ScheduleMailingCampaing(
                null,
                new Models.MailingList("listName"),
                DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ScheduleMailingCampaing_PastDate_ThrowsException()
        {
            // Arrange
            var mockApiGateway = new Mock<IReachMailApiGateway>();
            var mockCampaignService = new CampaignService(mockApiGateway.Object);

            // Act & Assert
            mockCampaignService.ScheduleMailingCampaing(
                new Models.Mailer("name", "fromEmail", "fromName", "replyToEmail", "subject", "content"),
                new Models.MailingList("listName"),
                DateTime.Now.AddSeconds(-1));
        }

        [TestMethod]
        public void ScheduleMailingCampaing_ValidArgs_ReturnsTrue()
        {
            // Arrange
            var mockApiGateway = new Mock<IReachMailApiGateway>();
            var mockCampaignService = new CampaignService(mockApiGateway.Object);
            mockApiGateway.Setup(m => m.ScheduleMailingCampaing(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .Returns(true);

            // Act
            var result = mockCampaignService.ScheduleMailingCampaing(
                new Models.Mailer("name", "fromEmail", "fromName", "replyToEmail", "subject", "content"),
                new Models.MailingList("listName"),
                DateTime.Now.AddDays(1));

            // Assert
            Assert.IsTrue(result);
        }
    }
}
