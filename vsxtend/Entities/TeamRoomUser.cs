using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vsxtend.Entities
{
    public class TeamRoomUser
    {
        public int roomId { get; set; }
        public string userId { get; set; }
        public LightweightAccount user { get; set; }
        public string lastActivity { get; set; }
        public string joinedDate { get; set; }
        public bool isOnline { get; set; }
    }
}
