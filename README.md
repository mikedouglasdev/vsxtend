vsxtend
=======

Lightweight Client Library for Visual Studio Online and Team Foundation Server REST APIs

This is the source code companion to the book, [Extending Team Foundation Server and Visual Studio Online](https://leanpub.com/tfsapibook)

Features include

- Full support for basic and OAuth authentication
- Implemented as a Portable Class Library to utilize across platforms
- Sample ASP.NET MVC usage
- Sample Webhook integration

Usage is very simple.  Utilize built in classes authentication, resource client, request and response objects.
```C#
// Arrange
        
BasicAuthentication authentication = new BasicAuthentication 
{ 
    Username = Helper.GetUsername(), 
    Password = Helper.GetPassword(), 
    Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection" 
};
TeamRoomClient teamroomClient = new TeamRoomClient(authentication);

// Act
CollectionResult<TeamRoom> response = await teamroomClient.GetRoomsAsync();

// Assert
Assert.IsNotNull(response);
Assert.IsNotNull(response.value);

```