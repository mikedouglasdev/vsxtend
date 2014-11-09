using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vsxtend.Samples.Mvc.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Uid { get; set; }
        public string UserName { get; set; }
        public string VsoToken { get; set; }
    }
}