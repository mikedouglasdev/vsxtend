using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vsxtend.Entities;

namespace Vsxtend.Samples.Mvc.ViewModels
{
    public class TeamRoomViewModel
    {
        private TeamRoom teamRoom;

        public TeamRoom TeamRoom
        {
            get
            {
                return this.teamRoom;
            }
            set
            {
                this.teamRoom = value;
            }
        }

        public CollectionResult<TeamRoomMessage> Messages { get; set; }
    }
}