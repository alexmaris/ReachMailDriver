using System;
using System.Collections.Generic;

namespace ReachMailDriver.Models
{
    public class MailingList
    {
        public Guid Id { get; set; }
        public String ListName { get; set; }

        public List<String> RecipientEmails { get; set; }

        public MailingList(String listName)
        {
            this.ListName = listName;
        }

        public MailingList(Guid id, String listName): this(listName)
        {
            this.Id = id;
            this.RecipientEmails = new List<string>();
        }

    }
}
