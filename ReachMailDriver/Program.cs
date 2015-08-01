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
            var gateway = new ReachMailApiGateway(ConfigurationManager.AppSettings["ReachMailAPIKey"]);

            var campaignService = new CampaignService(gateway);
            var mailerService = new MailerService(gateway);
            var listService = new MailingListService(gateway);

            Console.WriteLine("ReachMail API Test - creating a new list, mailer, and campaign." + Environment.NewLine);

            var mailList = listService.CreateMailingList(
                "Alex Test List " + DateTime.UtcNow.ToString(),
                emails);

            var mailer = mailerService.CreateMailer(
                "Hello World mailer",
                "alexandru.maris@gmail.com", 
                "Alex Maris", 
                "alexandru.maris@gmail.com", 
                "Hello World Mailer", 
                "Hello World!");

            campaignService.ScheduleMailingCampaing(mailer, mailList, DateTime.Now.AddSeconds(10));

            Console.WriteLine("Done!");
        }
    }
}
