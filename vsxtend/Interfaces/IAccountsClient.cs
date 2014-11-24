using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsxtend.Entities;

namespace Vsxtend.Interfaces
{
    public interface IAccountsClient
    {
        Task<CollectionResult<Account>> GetAccountsByOwnerIdAsync(Guid ownerId);

        Task<CollectionResult<Account>> GetAccountsByMemberIdAsync(Guid memberId);
    }
}
