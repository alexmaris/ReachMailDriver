using Reachmail;
using Reachmail.Campaigns.Post.Request;

using Reachmail.Mailings.Post.Request;
using Reachmail.Mailings.Post.Response;
using System;

namespace ReachMailDriver
{
    public class MailingCampaing
    {
        private Api reachMail;
        private String mailerName;
        private Mailing mailer;

        public MailingCampaing(String mailerName, Api reachMail)
        {
            if (String.IsNullOrEmpty(mailerName) || reachMail == null)
            {
                throw new ArgumentNullException("Null arguments not allowed.");
            }
            this.mailerName = mailerName;
            this.reachMail = reachMail;
        }

        /// <summary>
        /// Create a new mailing campaign
        /// </summary>
        /// <param name="listId">List ID that the campaign is targeting</param>
        /// <param name="mailingId">Mailer ID of the template to be used by the campaign</param>
        internal void createMailingCampaing(Guid listId)
        {
            if (mailer == null)
            {
                mailer = createMailer(mailerName);
            }

            var request = new QueueParameters
            {
                ListIds = new ListIds { 
                    listId
                },
                MailingId = mailer.Id,
                Properties = new Properties
                {
                    DeliveryTime = DateTime.Now.AddSeconds(5),
                }
            };

            var result = reachMail.Campaigns.Post(request);
        }

        /// <summary>
        /// Create a mailing 'template'
        /// </summary>
        /// <param name="name">Mailing name</param>
        internal Mailing createMailer(String name)
        {
            var mailerProp = new MailingProperties()
            {
                Name = name,
                MailingFormat = MailingFormat.Text,
                FromEmail = "amaris@outlook.com",
                FromName = "Alex Maris",
                ReplyToEmail = "amaris@outlook.com",
                Subject = "Alex Maris - Hello world mailer",
                TextContent = "Hi there!"
            };

            var mailing = reachMail.Mailings.Post(mailerProp);

            Console.WriteLine("Created mailer Guid: {0}", mailing.Id);

            return mailing;
        }
    }
}
