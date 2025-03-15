using IdentityServer.Entities.ApplicationUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.DAL.Context.Interfaces
{
    public interface IUserContext : IDbContextProvider
    {
        public DbSet<User> Users { get; }
    }
}
