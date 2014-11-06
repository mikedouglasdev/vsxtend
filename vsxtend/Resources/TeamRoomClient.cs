using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vsxtend.Common;
using Vsxtend.Entities;
using Vsxtend.Interfaces;


namespace Vsxtend.Resources
{
public class TeamRoomClient : ResourceHttpClient, ITeamRoomClient
{
    private readonly AuthenticationBase authentication;
    public TeamRoomClient(AuthenticationBase authentication)
    {
        this.authentication = authentication;
    }
        public async Task<TeamRoom> GetRoomAsync(int roomId)
        {
            return await base.GetAsync<TeamRoom>(authentication, 
                                    string.Format("https://{0}/_apis/chat/Rooms/{1}", 
                                    authentication.Account,roomId));
        }      
        
        public async Task<CollectionResult<TeamRoom>> GetRoomsAsync()
        {
            return await base.GetAsync<CollectionResult<TeamRoom>>(authentication, 
                                                                            string.Format("https://{0}/_apis/chat/Rooms", 
                                                                            authentication.Account));
        }

        public async Task JoinTeamRoomAsync(int roomId)
        {
            var connectionInfo = await GetConnectionDataAsync(authentication);

            var httpContent = GetConnectionHttpContent(connectionInfo);

            string address = string.Format("https://{0}/_apis/chat/Rooms/{1}/Users/{2}", authentication.Account, roomId, connectionInfo.authenticatedUser.id);

            await base.PutAsync(authentication, address, httpContent);

            return;
        }

        public async Task LeaveTeamRoomAsync(int roomId)
        {
            var connectionInfo = await GetConnectionDataAsync(authentication);

            var httpContent = GetConnectionHttpContent(connectionInfo);

            string address = string.Format("https://{0}/_apis/chat/Rooms/{1}/Users/{2}", authentication.Account, roomId, connectionInfo.authenticatedUser.id);

            await base.DeleteAsync(authentication, address);
        }

        public async Task<TeamRoom> CreateTeamRoomAsync(TeamRoom room)
        {
            string address = string.Format("https://{0}/_apis/chat/Rooms", authentication.Account);

            return await base.PostAsync<TeamRoom>(authentication, address, room);
        }

        public async Task<TeamRoom> UpdateTeamRoomAsync(int id, TeamRoom room)
        {
            string address = string.Format("https://{0}/_apis/chat/Rooms/{1}", authentication.Account, id.ToString());

            return await base.PatchAsync<TeamRoom>(authentication, address, room);
        }

public async Task DeleteTeamRoomAsync(int roomId)
{
    string address = string.Format("https://{0}/_apis/chat/Rooms/{1}", authentication.Account, roomId.ToString());

    await base.DeleteAsync(authentication, address);
}

        public async Task<CollectionResult<TeamRoomMessage>> GetMessagesAsync(int roomId, DateTime? lastRetrieved = null)
        {
            if (lastRetrieved.HasValue)
                lastRetrieved = lastRetrieved.Value.AddMilliseconds(100);
            string address = string.Format("https://{0}/_apis/chat/rooms/{1}/messages" + 
                (lastRetrieved.HasValue ? "?$filter=PostedTime%20gt%20" + lastRetrieved.Value.ToString("o") : ""), 
                authentication.Account, roomId.ToString());

            return await base.GetAsync<CollectionResult<TeamRoomMessage>>(authentication, address);
        }

        public async Task<CollectionResult<TeamRoomMessage>> GetMessagesAsync(int roomId, string filter)
        {
            string address = string.Format("https://{0}/_apis/chat/rooms/{1}/messages" +
                "?$filter=" + filter,
                authentication.Account, roomId.ToString());

            return await base.GetAsync<CollectionResult<TeamRoomMessage>>(authentication, address);
        }

        public async Task<TeamRoomMessage> GetMessageAsync(int roomId, int messageId)
        {
            string address = string.Format("https://{0}/_apis/chat/Rooms/{1}/messages/{2}", authentication.Account, roomId.ToString(), messageId.ToString());

            return await base.GetAsync<TeamRoomMessage>(authentication, address);
        }

        public async Task<CollectionResult<TeamRoomUser>> GetUsersAsync(int roomId)
        {
            string address = string.Format("https://{0}/_apis/chat/Rooms/{1}/users", authentication.Account, roomId.ToString());

            return await base.GetAsync<CollectionResult<TeamRoomUser>>(authentication, address);
        }

        public async Task<TeamRoomUser> GetUserAsync(int roomId, string userId)
        {
            string address = string.Format("https://{0}/_apis/chat/Rooms/{1}/users/{2}", authentication.Account, roomId.ToString(), userId);

                return await base.GetAsync<TeamRoomUser>(authentication, address);
        }

        public async Task<TeamRoomMessage> CreateMessageAsync(int roomId, string content)
        {
            string address = string.Format("https://{0}/_apis/chat/Rooms/{1}/messages", authentication.Account, roomId.ToString());

            var message = new TeamRoomMessage { content = content };
            return await base.PostAsync<TeamRoomMessage>(authentication, address, message);
        }

        public async Task<TeamRoomMessage> UpdateMessageAsync(int roomId, int messageId, string content)
        {
            string address = string.Format("https://{0}/_apis/chat/Rooms/{1}/messages/{2}", authentication.Account, roomId.ToString(), messageId.ToString());

            var message = new TeamRoomMessage { content = content };
            return await base.PatchAsync<TeamRoomMessage>(authentication, address, message);
        }

        public async Task DeleteMessageAsync(int roomId, int messageId)
        {
            string address = string.Format("https://{0}/_apis/chat/Rooms/{1}/messages/{2}", authentication.Account, roomId.ToString(), messageId.ToString());

            await base.DeleteAsync(authentication, address);
        }
    }
}