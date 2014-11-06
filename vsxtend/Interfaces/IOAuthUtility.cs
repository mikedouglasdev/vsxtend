using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsxtend.Entities;

namespace Vsxtend.Interfaces
{
    public interface IOAuthUtility
    {
        string GenerateAuthorizeUrl(string authorizationUrl, string applicationId, string redirectUrl, string state);

        string GeneratePostData(string applicationSecret, string token, string redirectUrl, bool isRenewal);

        Task<OAuthToken> GetOAuthToken(string tokenUrl, string applicationSecret, string token, string redirectUrl, bool isRenewal);
    }
}
