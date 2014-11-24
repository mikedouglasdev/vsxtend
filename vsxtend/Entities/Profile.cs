using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vsxtend.Entities
{
    public class Profile
    {
        public string displayName { get; set; }
        public string publicAlias { get; set; }
        public string emailAddress { get; set; }
        public int coreRevision { get; set; }
        public string timeStamp { get; set; }
        public string id { get; set; }
        public int revision { get; set; }
    }
}
