using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vsxtend.Entities
{
    public class VersionControlItem
    {
        public int BaseChangesetId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string Path { get; set; }
        public bool IsFolder { get; set; }
    }
}
