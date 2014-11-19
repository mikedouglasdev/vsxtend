using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Vsxtend.Common;
using Vsxtend.Entities;
using Vsxtend.UnitTests;
using System.Linq;
using Vsxtend.Resources;

namespace Vsxtend.UnitTests.Integration
{
    [TestClass]
    public class TeamRoomTests
    {
        [TestMethod]
        public async Task TeamRoom_GetRooms_ShouldReturnAllTeamRooms()
        {
            // Arrange
            RestHttpClient teamroomClient = new RestHttpClient();
            BasicAuthentication authentication = new BasicAuthentication 
            { 
                Username = Helper.GetUsername(), 
                Password = Helper.GetPassword(), 
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection" };

            

            // Act
            var response = await teamroomClient.GetAsync<CollectionResult<TeamRoom>>(authentication,
                                                                            string.Format("https://{0}/_apis/chat/Rooms",
                                                                            authentication.Account));
            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.value);
        }

        [TestMethod]
        public async Task TeamRoom_GetRoom_ShouldReturnTeamRoom54()
        {
            // Arrange
            RestHttpClient teamroomClient = new RestHttpClient();
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };
            int roomId = 54;

            // Act
            var response = await teamroomClient.GetAsync<TeamRoom>(authentication,
                                                                string.Format("https://{0}/_apis/chat/Rooms/{1}",
                                                                authentication.Account,
                                                                roomId));

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.id > 0);
        }

        // MD - Commented out because user account doesn't have enough rights
        [TestMethod]
        public async Task TeamRoom_CreateRoom_ShouldCreateNewRoom()
        {
            // Arrange
            
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };
            TeamRoomClient teamroomClient = new TeamRoomClient(authentication);

            // delete room if exists
            try
            {
                var teamRooms = await teamroomClient.GetRoomsAsync();
                var teamRoom = teamRooms.value.SingleOrDefault(r => r.name == "Unit Test Room 2");
                if (teamRoom != null)
                    await teamroomClient.DeleteTeamRoomAsync(teamRoom.id);
            }
            catch
            { }

            TeamRoom room = new TeamRoom { name = "Unit Test Room 2", description = "This is the unit test room." };

            // Act
            var response = await teamroomClient.CreateTeamRoomAsync(room);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.id > 0);
        }

        [TestMethod]
        public async Task TeamRoom_EditRoom_ShouldUpdateDescription()
        {
            // Arrange
            RestHttpClient teamroomClient = new RestHttpClient();
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };

            // Get Team Room to get Id
            string getTeamRoomsAddress = string.Format("https://{0}/_apis/chat/Rooms", authentication.Account);

            var response = await teamroomClient.GetAsync<CollectionResult<TeamRoom>>(authentication,
                                                                            string.Format("https://{0}/_apis/chat/Rooms",
                                                                            authentication.Account));

            var unitTestRoom = response.value.SingleOrDefault(p => p.name == "Unit Test Room 2") as TeamRoom;

            unitTestRoom.description = "Updated!";

            // Act
            string address = string.Format("https://{0}/_apis/chat/Rooms/{1}", authentication.Account, unitTestRoom.id);
            var editTeamRoom = await teamroomClient.PatchAsync<TeamRoom>(authentication, address, unitTestRoom);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(editTeamRoom.id > 0);
        }

[TestMethod]
public async Task TeamRoom_DeleteRoom_ShouldDeleteUnitTestRoom()
{
    // Arrange
    RestHttpClient teamroomClient = new RestHttpClient();
    BasicAuthentication authentication = new BasicAuthentication
    {
        Username = Helper.GetUsername(),
        Password = Helper.GetPassword(),
        Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
    };

    string getTeamRoomsAddress = string.Format("https://{0}/_apis/chat/Rooms", authentication.Account);

    var response = await teamroomClient.GetAsync<CollectionResult<TeamRoom>>(authentication,
                                                                    string.Format("https://{0}/_apis/chat/Rooms",
                                                                    authentication.Account));
            
    var unitTestRoom = response.value.SingleOrDefault(p => p.name == "Unit Test Room");

    if(unitTestRoom != null)
    { 
        int id = unitTestRoom.id;

        // act
        string address = string.Format("https://{0}/_apis/chat/Rooms/{1}", authentication.Account, id);
        await teamroomClient.DeleteAsync(authentication, address);
    }
}

        [TestMethod]
        public async Task TeamRoom_GetMessages_ShouldReturnMessagesInTeamRoom()
        {
            // Arrange
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };
            TeamRoomClient teamroomClient = new TeamRoomClient(authentication);


            DateTime? lastRetrieved = null;
            int roomId = 54;
            var response = await teamroomClient.GetMessagesAsync(54);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.count > 0);
        }

        [TestMethod]
        public async Task TeamRoom_GetMessagesFor0202_ShouldReturnMessagesInTeamRoom()
        {
            // Arrange
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };
            TeamRoomClient teamroomClient = new TeamRoomClient(authentication);


            DateTime? lastRetrieved = DateTime.Today.AddDays(-20);

            int roomId = 54;
            var response = await teamroomClient.GetMessagesAsync(54, lastRetrieved);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.count > 0);
        }


        [TestMethod]
        public async Task TeamRoom_JoinRoom54_UserShouldShowInList()
        {
            // Arrange
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };
            TeamRoomClient teamroomClient = new TeamRoomClient(authentication);

            int roomId = 54;

            await teamroomClient.LeaveTeamRoomAsync(roomId);

            await teamroomClient.JoinTeamRoomAsync(roomId);

            // do a get users and verify user is in the list
            var users = await teamroomClient.GetUsersAsync(roomId);

            Assert.IsNotNull(users);
            var user = users.value.SingleOrDefault(p => p.user.displayName == "TFS API Book - Demo Account" && p.isOnline);
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task TeamRoom_LeaveRoom54_ShouldShowUserIsNoLongerInRoom()
        {
            // Arrange
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };
            TeamRoomClient teamroomClient = new TeamRoomClient(authentication);

            int roomId = 54;

            await teamroomClient.JoinTeamRoomAsync(roomId);

            await teamroomClient.LeaveTeamRoomAsync(roomId);
            await Task.Delay(2000);
            // do a get users and verify user is not in the list
            var users = await teamroomClient.GetUsersAsync(roomId);

            Assert.IsNotNull(users);
            var user = users.value.SingleOrDefault(p => p.user.displayName == "TFS API Book - Demo Account" && p.isOnline);
            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task TeamRoom_GetUsersForTeam54_ShouldReturnAllUsers()
        {
            // Arrange
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };
            TeamRoomClient teamroomClient = new TeamRoomClient(authentication);
            
            int roomId = 54;

            await teamroomClient.JoinTeamRoomAsync(roomId);

            var users = await teamroomClient.GetUsersAsync(roomId);

            Assert.IsNotNull(users);
            var user = users.value.SingleOrDefault(p => p.user.displayName == "TFS API Book - Demo Account" && p.isOnline);
            Assert.IsNotNull(user);
        }

            [TestMethod]
            public async Task TeamRoom_GetUserTfsApiBookuser_ShouldReturnUserInfo()
            {
                // Arrange
                BasicAuthentication authentication = new BasicAuthentication
                {
                    Username = Helper.GetUsername(),
                    Password = Helper.GetPassword(),
                    Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
                };
                TeamRoomClient teamroomClient = new TeamRoomClient(authentication);

                int roomId = 54;

                await teamroomClient.JoinTeamRoomAsync(roomId);

                var users = await teamroomClient.GetUsersAsync(roomId);

                var lookupUser = users.value.SingleOrDefault(p => p.user.displayName == "TFS API Book - Demo Account" && p.isOnline);

                var user = await teamroomClient.GetUserAsync(roomId, lookupUser.userId);

                Assert.IsNotNull(user);
                Assert.AreEqual(lookupUser.user.displayName, user.user.displayName);
            }

        [TestMethod]
        public async Task TeamRoom_CreateMessage_ShouldWriteToRoom()
        {
            // Arrange
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };
            TeamRoomClient teamroomClient = new TeamRoomClient(authentication);

            int roomId = 54;    

            var message = await teamroomClient.CreateMessageAsync(roomId, @"PBI #1 has been created");

            var messages = await teamroomClient.GetMessagesAsync(roomId);
                
            Assert.IsNotNull(message);
            Assert.IsTrue(message.id > 0);
            Assert.IsNotNull(messages);
            var messageFound = messages.value.SingleOrDefault(p => p.id == message.id);
            Assert.IsNotNull(messageFound);

            // clean up
            await teamroomClient.DeleteMessageAsync(roomId, message.id);
        }

        // TFS API Demo account doesn't have rights to update/delete message
        [TestMethod]
        public async Task TeamRoom_UpdateMessage_ShouldUpdateContent()
        {
            // Arrange
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };
            TeamRoomClient teamroomClient = new TeamRoomClient(authentication);

            int roomId = 54;

            await teamroomClient.JoinTeamRoomAsync(roomId);

            var message = await teamroomClient.CreateMessageAsync(roomId, "This is a test, #1");
            await Task.Delay(2000);
            string updatedText = "This is updated, #1";
            await teamroomClient.UpdateMessageAsync(roomId, message.id, updatedText);

            var messages = await teamroomClient.GetMessagesAsync(roomId);

            Assert.IsNotNull(message);
            Assert.IsTrue(message.id > 0);
            Assert.IsNotNull(messages);
            var messageFound = messages.value.SingleOrDefault(p => p.id == message.id);
            Assert.IsNotNull(message);
            Assert.AreEqual(updatedText, messageFound.content);

            // clean up
            await teamroomClient.DeleteMessageAsync(roomId, message.id);
        }

        // TFS API Demo account doesn't have rights to delete message
[TestMethod]
public async Task TeamRoom_DeleteMessage_ShouldRemoveMessageFromRoom()
{
    // Arrange
    BasicAuthentication authentication = new BasicAuthentication
    {
        Username = Helper.GetUsername(),
        Password = Helper.GetPassword(),
        Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
    };
    TeamRoomClient teamroomClient = new TeamRoomClient(authentication);

    int roomId = 54;

    await teamroomClient.JoinTeamRoomAsync(roomId);

    var message = await teamroomClient.CreateMessageAsync(roomId, "This is another message to delete");

    await teamroomClient.DeleteMessageAsync(roomId, message.id);

    var messages = await teamroomClient.GetMessagesAsync(roomId);

    Assert.IsNotNull(message);
    Assert.IsTrue(message.id > 0);
    Assert.IsNotNull(messages);
    var messageFound = messages.value.SingleOrDefault(p => p.id == message.id);
    Assert.IsNull(messageFound);
}
    }

}
