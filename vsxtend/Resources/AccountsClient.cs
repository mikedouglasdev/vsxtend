using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsxtend.Common;
using Vsxtend.Entities;
using Vsxtend.Interfaces;

namespace Vsxtend.Resources
{
    public class AccountsClient : ResourceHttpClient, IAccountsClient
    {
        private readonly AuthenticationBase authentication;

        public AccountsClient(AuthenticationBase authentication)
        {
            this.authentication = authentication;
        }

        public async Task<CollectionResult<Account>> GetAccountsByOwnerIdAsync(Guid ownerId)
        {
            if (!(authentication is OAuthAuthorization))
                throw new NotSupportedException("GetAccountsByOwnerIdAsync() only is allowed with OAuth");

            return await base.GetAsync<CollectionResult<Account>>(authentication,
                     string.Format("https://app.vssps.visualstudio.com/_apis/Accounts?ownerId={0}", ownerId.ToString()));
        }

        public async Task<CollectionResult<Account>> GetAccountsByMemberIdAsync(Guid memberId)
        {
            if (!(authentication is OAuthAuthorization))
                throw new NotSupportedException("GetAccountsByMemberIdAsync() only is allowed with OAuth");

            return await base.GetAsync<CollectionResult<Account>>(authentication,
                     string.Format("https://app.vssps.visualstudio.com/_apis/Accounts?memberId={0}", memberId.ToString()));
        }
    }
}
