using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsxtend.Entities
{
    public class CollectionResult<T>
    {
        public int count { get; set; }
        public List<T> value { get; set; }
    }
}