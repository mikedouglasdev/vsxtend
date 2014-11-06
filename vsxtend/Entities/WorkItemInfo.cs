using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vsxtend.Entities
{
    public class Message
    {
        public string text { get; set; }
        public string html { get; set; }
        public string markdown { get; set; }
    }

    public class DetailedMessage
    {
        public string text { get; set; }
        public string html { get; set; }
        public string markdown { get; set; }
    }

    public class Field2
    {
        public int id { get; set; }
        public string refName { get; set; }
        public string name { get; set; }
    }

    public class Field
    {
        public Field2 field { get; set; }
        public string originalValue { get; set; }
        public string updatedValue { get; set; }
    }

    public class Update
    {
        public string url { get; set; }
        public string revisionUrl { get; set; }
        public int id { get; set; }
        public int rev { get; set; }
        public List<Field> fields { get; set; }
    }

    public class Field4
    {
        public int id { get; set; }
        public string refName { get; set; }
        public string name { get; set; }
    }

    public class Field3
    {
        public Field4 field { get; set; }
        public string value { get; set; }
    }

    public class Resource
    {
        public Update update { get; set; }
        public string updatesUrl { get; set; }
        public List<Field3> fields { get; set; }
        public List<object> links { get; set; }
        public List<object> resourceLinks { get; set; }
        public int id { get; set; }
        public int rev { get; set; }
        public string url { get; set; }
        public string webUrl { get; set; }
    }

    public class WorkItemInfo
    {
        public string subscriptionId { get; set; }
        public int notificationId { get; set; }
        public string id { get; set; }
        public string eventType { get; set; }
        public string publisherId { get; set; }
        public Message message { get; set; }
        public DetailedMessage detailedMessage { get; set; }
        public Resource resource { get; set; }
        public string createdDate { get; set; }
    }
}
