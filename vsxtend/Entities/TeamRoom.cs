using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsxtend.Entities
{
    public class TeamRoom
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string lastActivity { get; set; }
        public bool isClosed { get; set; }
        public string creatorUserTfId { get; set; }
        public LightweightAccount createdBy { get; set; }
        public string createdDate { get; set; }
    }
}