using System;

namespace ReachMailDriver.Models
{
    public class Mailer
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String FromEmail { get; set; }
        public String FromName { get; set; }
        public String ReplyToEmail { get; set; }
        public String Subject { get; set; }
        public String TextContent { get; set; }

        public MailingList RecipientList { get; set; }

        public Mailer(String name, String fromEmail, String fromName, String replyToEmail, String subject, String textContent)
        {
            this.Name = name;
            this.FromEmail = fromEmail;
            this.FromName = fromEmail;
            this.ReplyToEmail = replyToEmail;
            this.Subject = subject;
            this.TextContent = textContent;
        }

        public Mailer(Guid id, String name, String fromEmail, String fromName, String replyToEmail, String subject, String textContent) :
            this(name, fromEmail, fromName, replyToEmail, subject, textContent)
        {
            this.Id = id;
        }
    }
}
