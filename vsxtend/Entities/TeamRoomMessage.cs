using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsxtend.Entities
{
    public class TeamRoomMessage
    {
        public int id { get; set; }
        public string content { get; set; }
        public string messageType { get; set; }
        public DateTime postedTime { get; set; }
        public int postedRoomId { get; set; }
        public string postedByUserTfid { get; set; }
        public string DefaultCollection { get; set; }
        public LightweightAccount postedBy { get; set; }
    }

}