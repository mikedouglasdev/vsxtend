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
    public class TfvcVersionControlTests
    {
        [TestMethod]
        public async Task Tfvc_GetTopLevelItems_ReturnsTopLevelChangesets()
        {
            // Arrange
            RestHttpClient restClient = new RestHttpClient();
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };



            // Act
            var response = await restClient.GetAsync<CollectionResult<VersionControlItem>>(authentication,
                                                                            string.Format("https://{0}/_apis/tfvc/items?scopePath=$/TFS+API+BOOK/Demo&recursionLevel=Full",
                                                                            authentication.Account));
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.value.Count > 0);
        }


        [TestMethod]
        public async Task Tfvc_GetMostRecent_ReturnsMostRecentChangeset()
        {
            // Arrange
            RestHttpClient restClient = new RestHttpClient();
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };



            // Act
            var response = await restClient.GetAsync<CollectionResult<Changeset>>(authentication,
                                                                            string.Format("https://{0}/_apis/tfvc/changesets/?scopePath=$/TFS+API+BOOK/Demo&$top=1",
                                                                            authentication.Account));
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.value.Count == 1);
        }

        [TestMethod]
        public async Task Tfvc_GetChangeset_ReturnsASpecificChangeset()
            {
            // Arrange
            RestHttpClient restClient = new RestHttpClient();
            BasicAuthentication authentication = new BasicAuthentication
            {
                Username = Helper.GetUsername(),
                Password = Helper.GetPassword(),
                Account = Helper.GetAccount() + ".visualstudio.com/DefaultCollection"
            };



            // Act
            var response = await restClient.GetAsync<Changeset>(authentication,
                                                                            string.Format("https://{0}/_apis/tfvc/changesets/26",
                                                                            authentication.Account));
            // Assert
            Assert.IsNotNull(response);
        }
    }
}