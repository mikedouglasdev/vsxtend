using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Vsxtend.Interfaces;
using Vsxtend.Common;
using Vsxtend.Resources;
using System.Globalization;
using Vsxtend.Entities;
using Vsxtend.Samples.Mvc.Services;

namespace Vsxtend.Samples.Mvc.Hubs
{
public class TeamRoomHub : Hub
{
    private ITeamRoomClient _teamRoomClient;
    
    public TeamRoomHub(ITeamRoomClient teamRoomClient)
    {
        _teamRoomClient = teamRoomClient;
    }

    public void Send(string message, int roomId)
    {
        _teamRoomClient.CreateMessageAsync(roomId, message);
    }

    public void GetMessages(int roomId, DateTime? dateTime)
    {
        CollectionResult<TeamRoomMessage> messages ;

        messages = _teamRoomClient.GetMessagesAsync(roomId, dateTime).Result;

        foreach (var message in messages.value)
        {
            Clients.Caller.addNewMessageToPage(message.postedBy.displayName, message.content, message.postedTime);
        }
    }
}
}