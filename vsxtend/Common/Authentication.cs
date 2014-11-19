using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsxtend.Common
{
    public abstract class AuthenticationBase
    {
        public string Account { get; set; }

        string apiVersion = "api-version=1.0";
        public string ApiVersion
        {
            get
            {
                return apiVersion;
            }
            set
            {
                apiVersion = value;
            }
        }
    }
    public class BasicAuthentication : AuthenticationBase
    {
        public string Username { get; set; }
        public string Password { get; set; }

        //public string Account { get; set; }
    }

    public class OAuthAuthorization : AuthenticationBase
    {
        [JsonProperty(PropertyName = "access_token")]
        public String accessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public String tokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public String expiresIn { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public String refreshToken { get; set; }

        public String Error { get; set; }
    }
}