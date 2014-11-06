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
        Task<T> GetAsync<T>(AuthenticationBase authentication, string address);

        Task PutAsync(AuthenticationBase authentication, string address, StringContent content);

        Task<T> PutAsync<T>(AuthenticationBase authentication, string address, StringContent content);

        Task<T> PostAsync<T>(AuthenticationBase authentication, string address, T model);

        Task<T> PatchAsync<T>(AuthenticationBase authentication, string address, T model);

        Task DeleteAsync(AuthenticationBase authentication, string address);
    }
}
