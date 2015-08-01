using Reachmail;
using Reachmail.Campaigns.Post.Request;
using ReachMailDriver.Models;
using ReachMailDriver.Services.Gateway;
using ReachMailDriver.Util;
using System;

namespace ReachMailDriver.Services
{
    public class CampaignService : AbstractDriverService
    {
        public CampaignService(IReachMailApiGateway apiGateway)
            : base(apiGateway)
        {
        }

        public bool ScheduleMailingCampaing(Mailer mailer, MailingList mailingList, DateTime dateTime)
        {
            Validate.Begin()
                .IsNotNull(mailer, "mailer")
                .IsNotNull(mailingList, "mailingList")
                .IsNotInPast(dateTime, "dateTime");

            return apiGateway.ScheduleMailingCampaing(mailer.Id, mailingList.Id, dateTime);
        }
    }
}
