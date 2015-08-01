using ReachMailDriver.Services;
using ReachMailDriver.Services.Gateway;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ReachMailDriver
{
    class Program
    {
        private readonly static List<String> emails = new List<string>{
            "alexandru.maris@gmail.com",
            "amaris@outlook.com"
        };

        static void Main(string[] args)
        {

            // Instantiate required services and the ReachMailApiGateway with a valid API key
            var gateway = new ReachMailApiGateway(ConfigurationManager.AppSettings["ReachMailAPIKey"]);

            var campaignService = ServiceFactory.Instance.getService<CampaignService>(gateway);
            var mailerService = ServiceFactory.Instance.getService<MailerService>(gateway);
            var listService = ServiceFactory.Instance.getService<MailingListService>(gateway);

            Console.WriteLine("ReachMail API Test - creating a new list, mailer, and campaign." + Environment.NewLine);

            // Create MailingList
            var mailList = listService.CreateMailingList(
                "Alex Test List " + DateTime.UtcNow.ToString(),
                emails);

            // Create Mailer
            var mailer = mailerService.CreateMailer(
                "Hello World mailer",
                "alexandru.maris@gmail.com",
                "Alex Maris",
                "alexandru.maris@gmail.com",
                "Hello World Mailer",
                "Hello World!");

            // Schedule Mailer
            campaignService.ScheduleMailingCampaing(mailer, mailList, DateTime.Now.AddSeconds(10));

            Console.WriteLine("Done - check your email!");
            Console.ReadKey();
        }
    }
}
