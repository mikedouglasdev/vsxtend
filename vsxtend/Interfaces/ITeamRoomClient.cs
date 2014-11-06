using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsxtend.Entities;

namespace Vsxtend.Interfaces
{
    public interface ITeamRoomClient
    {
        Task<TeamRoom> GetRoomAsync(int roomId);

        Task<CollectionResult<TeamRoom>> GetRoomsAsync();

        Task JoinTeamRoomAsync(int roomId);

        Task LeaveTeamRoomAsync(int roomId);

        Task<TeamRoom> CreateTeamRoomAsync(TeamRoom room);

        Task<TeamRoom> UpdateTeamRoomAsync(int id, TeamRoom room);

        Task<CollectionResult<TeamRoomMessage>> GetMessagesAsync(int roomId, DateTime? lastRetrieved = null);

        Task DeleteTeamRoomAsync(int roomId);

        Task<TeamRoomMessage> CreateMessageAsync(int roomId, string content);

        Task<CollectionResult<TeamRoomMessage>> GetMessagesAsync(int roomId, string filter);
    }
}
