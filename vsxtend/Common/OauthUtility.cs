using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vsxtend.Entities;
using Vsxtend.Interfaces;

namespace Vsxtend.Common
{
    public class OAuthUtility : RestHttpClient, IOAuthUtility
    {
        private readonly AuthenticationBase Authentication;

        public OAuthUtility(AuthenticationBase authentication)
        {
            this.Authentication = authentication;
        }

        public string GenerateAuthorizeUrl(string authorizationUrl, string applicationId, string redirectUrl, string state)
        {
            return String.Format("{0}?client_id={1}&response_type=Assertion&state={2}&scope=preview_api_all%20preview_msdn_licensing&redirect_uri={3}",
                authorizationUrl,
                applicationId,
                state,
                redirectUrl
                );
        }

        public string GeneratePostData(string applicationSecret, string token, string redirectUrl, bool isRenewal)
        {
            if (isRenewal)
            {
                return string.Format("client_assertion_type=urn:ietf:params:oauth:client-assertion-type:jwt-bearer&client_assertion={0}&grant_type=refresh_token&assertion={1}&redirect_uri={2}",
                    Uri.EscapeUriString(applicationSecret),
                    Uri.EscapeUriString(token),
                    redirectUrl
                    );

            }
            else
            {
                return string.Format("client_assertion_type=urn:ietf:params:oauth:client-assertion-type:jwt-bearer&client_assertion={0}&grant_type=urn:ietf:params:oauth:grant-type:jwt-bearer&assertion={1}&redirect_uri={2}",
                    Uri.EscapeUriString(applicationSecret),
                    Uri.EscapeUriString(token),
                    redirectUrl
                    );
            }
        }

        public async Task<OAuthToken> GetOAuthToken(string tokenUrl, string applicationSecret, string token, string redirectUrl, bool isRenewal)
        {
            string oauthPostData = GeneratePostData(applicationSecret, token, redirectUrl, isRenewal);
            string responseData = string.Empty;
            OAuthToken oauthToken = null;
            string error = string.Empty;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(tokenUrl);

            webRequest.Method = "POST";
            //webRequest.ContentLength = strPostData.Length;
            webRequest.ContentType = "application/x-www-form-urlencoded";

            using (StreamWriter swRequestWriter = new StreamWriter(await webRequest.GetRequestStreamAsync()))
            {
                swRequestWriter.Write(oauthPostData);
            }

            try
            {
                HttpWebResponse hwrWebResponse = (HttpWebResponse)(await webRequest.GetResponseAsync());

                if (hwrWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader srResponseReader = new StreamReader(hwrWebResponse.GetResponseStream()))
                    {
                        responseData = srResponseReader.ReadToEnd();
                    }

                    oauthToken = JsonConvert.DeserializeObject<OAuthToken>(responseData);

                    return oauthToken;
                }

                error = "<strong>Issue:</strong> " + hwrWebResponse.StatusCode + "::" + hwrWebResponse.StatusDescription;

            }
            catch (WebException wex)
            {
                error = "<strong>Request Issue:</strong> " + wex.Message.ToString();
            }
            catch (Exception ex)
            {
                error = "<strong>Issue:</strong> " + ex.Message.ToString();
            }

            OAuthToken emptyToken = new OAuthToken();
            emptyToken.Error = error;

            return emptyToken;
        }
    }
}
