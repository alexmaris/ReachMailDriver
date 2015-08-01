using Reachmail;
using Reachmail.Campaigns.Post.Request;
using Reachmail.Lists.Post.Request;
using Reachmail.Lists.Recipients.PostByListId.Request;
using Reachmail.Mailings.Post.Request;
using ReachMailDriver.Util;
using System;
using System.Collections.Generic;

namespace ReachMailDriver.Services.Gateway
{
    public class ReachMailApiGateway : IReachMailApiGateway
    {
        private Api reachMail;

        public ReachMailApiGateway(string apiKey)
        {
            Validate.Begin().IsNotNullOrEmpty(apiKey, "reachMail");

            reachMail = Reachmail.Api.Create(apiKey);
        }

        public bool AddRecipientEmails(Guid listId, ICollection<String> recipientEmails)
        {
            if (listId == Guid.Empty) throw new ArgumentNullException("listId");

            foreach (String email in recipientEmails)
            {
                reachMail.Lists.Recipients.Post(listId, new RecipientProperties
                {
                    Email = email
                });
            }

            return true;
        }

        public bool ScheduleMailingCampaing(Guid mailerId, Guid mailingListId, DateTime dateTime)
        {
            var response = reachMail.Campaigns.Post(new QueueParameters
            {
                ListIds = new ListIds {
                    mailingListId
                },
                MailingId = mailerId,
                Properties = new Reachmail.Campaigns.Post.Request.Properties
                {
                    DeliveryTime = dateTime,
                }
            });

            if (response.Id == Guid.Empty) throw new Exception("MailingCampaign was not succesfully scheduled.");

            return true;
        }

        public Guid CreateMailer(String name, String fromEmail, String fromName, String replyToEmail, String subject, String textContent)
        {
            var response = reachMail.Mailings.Post(new MailingProperties()
            {
                Name = name,
                MailingFormat = MailingFormat.Text,
                FromEmail = fromEmail,
                FromName = fromName,
                ReplyToEmail = replyToEmail,
                Subject = subject,
                TextContent = textContent
            });

            if (response.Id == Guid.Empty)
                throw new Exception("Mailer campaign could not be  not succesfully created.");

            return response.Id;
        }

        public Guid CreateMailingList(String listName)
        {
            var response = reachMail.Lists.Post(new ListProperties
            {
                Name = listName,
                Type = ListType.Recipient
            });

            if (response.Id == Guid.Empty)
                throw new Exception("Mailing list could not be  not succesfully created.");

            return response.Id;
        }
    }
}
