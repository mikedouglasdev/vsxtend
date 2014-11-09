using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Vsxtend.Entities;
using Vsxtend.Interfaces;

namespace Vsxtend.Samples.Mvc.Services
{
public class TeamRoomService : ITeamRoomService
{
    private ITeamRoomClient TeamRoomClient { get; set; }

    public TeamRoomService(ITeamRoomClient teamRoomClient)
    {
        this.TeamRoomClient = teamRoomClient;
    }

    public async Task<CollectionResult<TeamRoomMessage>>  GetMessagesAsync(int roomId, DateTime? dateTime, bool hideEnterExitMessages = false)
    {
        var messages = await this.TeamRoomClient.GetMessagesAsync(roomId, dateTime);

        if (hideEnterExitMessages)
        {
            messages.value.RemoveAll(m => m.postedBy.id == "39a32a5e-6bbd-4d4f-af43-2d16b78d26d5" && m.content.Contains("left the room"));

            messages.value.RemoveAll(m => m.postedBy.id == "39a32a5e-6bbd-4d4f-af43-2d16b78d26d5" && m.content.Contains("entered the room"));

            messages.count = messages.value.Count;
        }

        return messages;
    }

    public async Task<CollectionResult<TeamRoomMessage>> GetMessagesForDateAsync(int roomId, DateTime date)
    {
        string filter = "PostedTime%20ge%20" + date.Date.ToString("o") + "%20and%20PostedTimed%20lt%20" + date.AddDays(1).Date.ToString("o");
        return await TeamRoomClient.GetMessagesAsync(roomId, filter);
    }
}

public interface ITeamRoomService
{
    Task<CollectionResult<TeamRoomMessage>> GetMessagesAsync(int roomId, DateTime? dateTime, bool hideEnterExitMessages = false);

    Task<CollectionResult<TeamRoomMessage>> GetMessagesForDateAsync(int roomId, DateTime date);
}
}