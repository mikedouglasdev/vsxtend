using System.Web.Http;
using System;
using Vsxtend.Entities;
using VSOWebHooks.Common;

namespace VSOWebHook.Controllers
{
    public class WorkItemsController : ApiController
    {
        /// <summary>
        /// A ping method which can be used to test the service.
        /// </summary>
        /// <returns>The current server time.</returns>
        public string GetPing()
        {
            return string.Format("Current server time is: {0}", DateTime.Now);
        }

        // POST: api/WorkItems
        [HttpPost]
        public void Post([FromBody] WorkItemInfo workItemInfo)
        {
            if (workItemInfo.eventType == "workitem.created")
            {
                WorkItemCreated(workItemInfo);
            }
            else if (workItemInfo.eventType == "workitem.updated")
            {
                WorkItemUpdated(workItemInfo);
            }
            else if (workItemInfo.eventType == "workitem.commented")
            {
                this.WorkItemUpdated(workItemInfo);
            }
        }

        private void WorkItemCreated(WorkItemInfo workItemInfo)
        {
            // TODO: process code for new work items
            var mailHelper = new MailHelper();

            mailHelper.SendMessage(
                "name@domain.com",
                "VSO WebHooks Test Message - Work Item Created",
                "name@domain.com",
                workItemInfo.detailedMessage.html,
                "smtp.office365.com",
                587,
                "<password>"
                );
        }

        private void WorkItemUpdated(WorkItemInfo workItemInfo)
        {
            // TODO: process code for updated work items
            var mailHelper = new MailHelper();

            mailHelper.SendMessage(
                "name@domain.com",
                "VSO WebHooks Test Message - Work Item Updated",
                "name@domain.com",
                workItemInfo.detailedMessage.html,
                "smtp.office365.com",
                587,
                "<password>"
                );
        }
    }
}