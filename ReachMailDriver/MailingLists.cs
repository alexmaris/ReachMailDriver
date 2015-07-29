using Reachmail;
using Reachmail.Lists.Post.Request;
using Reachmail.Lists.Post.Response;
using Reachmail.Lists.Recipients.Filtered.PostByListId.Request;
using Reachmail.Lists.Recipients.PostByListId.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReachMailDriver
{
    public class MailingLists
    {

        private String listName;
        private Api reachMail;

        public MailingLists(String listName, Api reachMail)
        {
            if (String.IsNullOrEmpty(listName) || reachMail == null)
            {
                throw new ArgumentNullException("Null arguments not allowed.");
            }

            this.listName = listName;
            this.reachMail = reachMail;
        }

        public List mailingList { get; set; }

        /// <summary>
        /// Create a ReachMail email list
        /// </summary>
        internal List createList()
        {
            var request = new ListProperties
            {
                Name = listName,
                Type = ListType.Recipient
            };

            var result = reachMail.Lists.Post(request);

            Console.WriteLine("List Guid: {0}", result.Id);

            var queryLists = reachMail.Lists.Filtered.Post(new Reachmail.Lists.Filtered.Post.Request.ListFilter { NewerThan = DateTime.Now.AddDays(-1) });
            var queryList = queryLists.FirstOrDefault(x => x.Id == result.Id);

            Console.WriteLine("Created list: {0}", queryList.Name);

            return result;
        }

        /// <summary>
        /// Add a recipient to the current ReachMail list
        /// </summary>
        /// <param name="emails">Collection of email addresses to add</param>
        public void addRecipientsToList(ICollection<String> emails)
        {
            if (this.mailingList == null)
            {
                this.mailingList = createList();
            }

            foreach (String email in emails)
            {
                reachMail.Lists.Recipients.Post(this.mailingList.Id,
                new RecipientProperties
                {
                    Email = email
                });
            }

            var queryLists = reachMail.Lists.Recipients.Filtered.Post(this.mailingList.Id,
                new RecipientFilter()
                {
                    NewerThan = DateTime.Now.AddDays(-1)
                });

            foreach (var item in queryLists)
            {
                Console.WriteLine("Added recipient to list: {0}", item.Email);
            }

            //reachMail.Lists.Recipients.Subscribe.Post(listId, new Reachmail.Lists.Recipients.Subscribe.PostByListId.Request.RecipientParameters()
            //{
            //    Properties = new Reachmail.Lists.Recipients.Subscribe.PostByListId.Request.RecipientParametersProperties
            //    {
            //        Email = "alexandru.maris@gmail.com"
            //    },
            //    Filter = new Reachmail.Lists.Recipients.Subscribe.PostByListId.Request.Filter
            //    {
            //        NewerThan = DateTime.Now.AddDays(-1)
            //    }
            //});
        }
    }
}
