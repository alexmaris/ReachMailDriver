using ReachMailDriver.Models;
using ReachMailDriver.Services.Gateway;
using ReachMailDriver.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReachMailDriver.Services
{
    public class MailingListService : AbstractDriverService
    {
        public MailingListService(IReachMailApiGateway apiGateway)
            : base(apiGateway)
        {
        }

        public MailingList CreateMailingList(String listName)
        {
            Validate.Begin()
                .IsNotNullOrEmpty(listName, "listName");

            Guid listId = apiGateway.CreateMailingList(listName);

            return new MailingList(listId, listName);
        }

        public MailingList CreateMailingList(String listName, ICollection<String> recipientEmails)
        {
            var mailingList = CreateMailingList(listName);

            apiGateway.AddRecipientEmails(mailingList.Id, recipientEmails);

            mailingList.RecipientEmails = recipientEmails.ToList();

            return mailingList;
        }



    }
}
