using IdentityServer.DAL.Context.Interfaces;
using IdentityServer.Entities.ApplicationUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {


        public Task CreateAsync(User user, CancellationToken cancellationToken);


        public Task DeleteAsync(Guid id, CancellationToken cancellationToken);


        public Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);


        public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);
     

        public Task<User> UpdateAsync(User user, CancellationToken cancellationToken);

    }
}
