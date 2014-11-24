using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vsxtend.Entities
{
    public class Account
    {
        public string accountId { get; set; }
        public string accountUri { get; set; }
        public string accountName { get; set; }
        public string organizationName { get; set; }
        public string accountType { get; set; }
        public string accountOwner { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string accountStatus { get; set; }
        public string lastUpdatedBy { get; set; }
        public Properties properties { get; set; }
        public string lastUpdatedDate { get; set; }

    }
}
