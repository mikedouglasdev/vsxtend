using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vsxtend.Entities
{
    public class Changeset
    {
        public int ChangesetId { get; set; }
        public Author Author { get; set; }
        public CheckedInBy CheckedInBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
    }
}