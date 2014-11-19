using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vsxtend.Common;

namespace Vsxtend.Interfaces
{
    public interface IRestHttpClient
    {
        Task<T> GetAsync<T>(AuthenticationBase authentication, string address, string apiVersion = "");

        Task PutAsync(AuthenticationBase authentication, string address, StringContent content, string apiVersion = "");

        Task<T> PutAsync<T>(AuthenticationBase authentication, string address, StringContent content, string apiVersion = "");

        Task<T> PostAsync<T>(AuthenticationBase authentication, string address, T model, string apiVersion = "");

        Task<T> PatchAsync<T>(AuthenticationBase authentication, string address, T model, string apiVersion = "");

        Task DeleteAsync(AuthenticationBase authentication, string address, string apiVersion = "");
    }
}
