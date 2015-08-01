using Reachmail;
using Reachmail.Mailings.Post.Request;
using ReachMailDriver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReachMailDriver.Util;
using ReachMailDriver.Services.Gateway;

namespace ReachMailDriver.Services
{
    public class MailerService : AbstractDriverService
    {
        public MailerService(IReachMailApiGateway apiGateway)
            : base(apiGateway)
        {
        }

        public Mailer CreateMailer(String name, String fromEmail, String fromName, String replyToEmail, String subject, String textContent)
        {
            Validate.Begin()
                .IsNotNullOrEmpty(name, "name")
                .IsNotNullOrEmpty(fromEmail, "fromEmail")
                .IsNotNullOrEmpty(fromName, "fromName")
                .IsNotNullOrEmpty(replyToEmail, "replyToEmail")
                .IsNotNullOrEmpty(subject, "subject")
                .IsNotNullOrEmpty(textContent, "textContent");

            var mailerId = apiGateway.CreateMailer(name, fromEmail, fromName, replyToEmail, subject, textContent);

            return new Mailer(mailerId, name, fromEmail, fromName, replyToEmail, subject, textContent);
        }
    }
}
