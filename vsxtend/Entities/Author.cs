using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vsxtend.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string UniqueName { get; set; }
    }
}