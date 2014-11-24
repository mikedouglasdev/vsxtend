using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsxtend.Entities;

namespace Vsxtend.Interfaces
{
    public interface IProfileClient
    {
        Task<Profile> GetMyProfile();
    }
}
