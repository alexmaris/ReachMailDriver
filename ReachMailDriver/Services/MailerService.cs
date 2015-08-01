using ReachMailDriver.Models;
using ReachMailDriver.Services.Gateway;
using ReachMailDriver.Util;
using System;

namespace ReachMailDriver.Services
{
    public class MailerService : AbstractDriverService
    {
        public MailerService(IReachMailApiGateway apiGateway)
            : base(apiGateway)
        {
        }

        virtual public Mailer CreateMailer(String name, String fromEmail, String fromName, String replyToEmail, String subject, String textContent)
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
