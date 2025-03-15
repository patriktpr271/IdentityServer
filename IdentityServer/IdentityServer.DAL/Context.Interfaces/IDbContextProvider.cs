using Microsoft.EntityFrameworkCore;

namespace IdentityServer.DAL.Context.Interfaces
{
    public interface IDbContextProvider
    {
        public DbContext Context { get; }
    }
}
