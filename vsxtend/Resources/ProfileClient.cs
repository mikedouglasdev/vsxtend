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
    public class ProfileClient : ResourceHttpClient, IProfileClient
    {
        private readonly AuthenticationBase authentication;

        public ProfileClient(AuthenticationBase authentication)
        {
            this.authentication = authentication;
        }

        public async Task<Profile> GetMyProfile()
        {
            if(!(authentication is OAuthAuthorization))
                throw new NotSupportedException("GetMyProfile() only is allowed with OAuth");

            return await base.GetAsync<Profile>(authentication,
                     "https://app.vssps.visualstudio.com/_apis/profile/profiles/me");
        }
    }
}
