using System;
using System.Collections.Generic;

namespace ReachMailDriver.Services.Gateway
{
    public interface IReachMailApiGateway
    {
        bool AddRecipientEmails(Guid listId, ICollection<String> recipientEmails);
        bool ScheduleMailingCampaing(Guid mailerId, Guid mailingListId, DateTime dateTime);
        Guid CreateMailer(String name, String fromEmail, String fromName, String replyToEmail, String subject, String textContent);
        Guid CreateMailingList(String listName);

    }
}