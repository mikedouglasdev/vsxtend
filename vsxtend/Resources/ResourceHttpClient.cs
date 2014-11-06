using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vsxtend.Common;
using Vsxtend.Entities;


namespace Vsxtend.Resources
{
    public class ResourceHttpClient : RestHttpClient
    {
        protected async Task<AuthenticatedUserResult> GetConnectionDataAsync(AuthenticationBase auth)
        {
                RestHttpClient client = new RestHttpClient();

                string address = string.Format("https://{0}/_apis/ConnectionData", auth.Account);

                var result = await base.GetAsync<AuthenticatedUserResult>(auth, address);

                return result;
        }

        protected StringContent GetConnectionHttpContent(AuthenticatedUserResult connectionInfo)
        {
            return new StringContent(
                @"{""UserId"": """ + connectionInfo.authenticatedUser.id + "\" }",
                Encoding.UTF8,
                "application/json");
        }
    }
}