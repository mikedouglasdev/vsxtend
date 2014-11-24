using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vsxtend.Interfaces;


namespace Vsxtend.Common
{
    public class RestHttpClient : IRestHttpClient
    {
        public async Task<T> GetAsync<T>(AuthenticationBase authentication, string address, string apiVersion = "")
        {
            if (apiVersion == "")
                apiVersion = authentication.ApiVersion;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue(authentication);

                using (HttpResponseMessage response = client.GetAsync(GetAddressQueryString(address, apiVersion)).Result)
                {
                    // will throw an exception if not successful
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                        throw new Exception(responseBody);

                    try
                    {
                        var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody);
                        return resultObject;
                    }
                    catch(Exception)
                    {
                        throw new Exception("Unable to convert results: " + responseBody);
                    }
                }
            }
        }

        public async Task PutAsync(AuthenticationBase authentication, string address, StringContent content, string apiVersion = "")
        {
            if (apiVersion == "")
                apiVersion = authentication.ApiVersion;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue(authentication);

                using (HttpResponseMessage response = await client.PutAsync(GetAddressQueryString(address, apiVersion), content))
                {
                    // will throw an exception if not successful
                    response.EnsureSuccessStatusCode();

                    return;
                }
            }
        }
        public async Task<T> PutAsync<T>(AuthenticationBase authentication, string address, StringContent content, string apiVersion = "")
        {
            if (apiVersion == "")
                apiVersion = authentication.ApiVersion;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue(authentication);

                using (HttpResponseMessage response = await client.PutAsync(GetAddressQueryString(address, apiVersion), content))
                {
                    // will throw an exception if not successful
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody);
                    return resultObject; 
                }
            }
        }

        public async Task<T> PostAsync<T>(AuthenticationBase authentication, string address, T model, string apiVersion = "")
        {
            if (apiVersion == "")
                apiVersion = authentication.ApiVersion;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue(authentication);
              
                string jsonModel = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(GetAddressQueryString(address, apiVersion), content))
                {
                    // will throw an exception if not successful
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (typeof(T) == typeof(string))
                        return (T)(object)responseBody;

                    var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody);
                    return resultObject;
                }
            }
        }

        
        public async Task<T> PatchAsync<T>(AuthenticationBase authentication, string address, T model, string apiVersion = "")
        {
            if (apiVersion == "")
                apiVersion = authentication.ApiVersion;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue(authentication);

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), GetAddressQueryString(address, apiVersion));
                string jsonModel = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                request.Content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    // will throw an exception if not successful
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody);
                    return resultObject;
                }
            }
        }

        public async Task DeleteAsync(AuthenticationBase authentication, string address, string apiVersion = "")
        {
            if (apiVersion == "")
                apiVersion = authentication.ApiVersion;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue(authentication);

                using (HttpResponseMessage response = await client.DeleteAsync(GetAddressQueryString(address, apiVersion)))
                {
                    // will throw an exception if not successful
                    response.EnsureSuccessStatusCode();

                    return;
                }
            }
        }

        private AuthenticationHeaderValue GetAuthenticationHeaderValue(AuthenticationBase authentication)
        {
            var basicAuthentication = authentication as BasicAuthentication;

            if (basicAuthentication != null)
            {
                return new AuthenticationHeaderValue("Basic",
                            Convert.ToBase64String(
                                System.Text.UTF8Encoding.UTF8.GetBytes(
                                    string.Format("{0}:{1}", basicAuthentication.Username, basicAuthentication.Password))));

            }

            var oauthAuthorization = authentication as OAuthAuthorization;

            if (oauthAuthorization != null)
            {
                return new AuthenticationHeaderValue("Bearer",
                        oauthAuthorization.accessToken);
            }

            throw new InvalidCastException("Authentication type is unknown.");
        }

        private string GetAddressQueryString(string address, string parameter)
        {
            if (parameter.Length == 0)
                return address;

            string seperator = address.IndexOf("?") > 0 ? "&" : "?";

            return string.Format("{0}{1}{2}", address, seperator, parameter);
        }
    }
}
