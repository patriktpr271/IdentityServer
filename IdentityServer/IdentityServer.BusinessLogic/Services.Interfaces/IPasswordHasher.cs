using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.BusinessLogic.Services.Interfaces
{
    interface IPasswordHasher
    {
        public string Hash(string password);

        public bool Verify(string hashString, string password);
    }
}
