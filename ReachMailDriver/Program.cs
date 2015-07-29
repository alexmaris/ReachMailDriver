using System;
using System.Collections.Generic;

namespace ReachMailDriver
{
    class Program
    {
        //TODO: hide this secret, don't share
        private readonly static String apiKey = "pApaxtFpKUEWzpE82-P9s-GIeZRcdhPOp7uS8JKr0FKWjwA1HmEGvz6TjAT8bsQ2";

        private readonly static List<String> emails = new List<string>{
            "alexandru.maris@gmail.com",
            "amaris@outlook.com",
            "bkothe@reachmail.com"
        };


        static void Main(string[] args)
        {

            Console.WriteLine("ReachMail API Test - creating a new list, mailer, and campaign." + Environment.NewLine);

            var reachmail = Reachmail.Api.Create(apiKey);

            var mailList = new MailingLists("Alex Test List " + DateTime.UtcNow.ToString(), reachmail);
            mailList.addRecipientsToList(emails);

            var campaign = new MailingCampaing("Hello World mailer", reachmail);
            campaign.createMailingCampaing(mailList.mailingList.Id);

            Console.WriteLine("Done!");
        }
    }
}
