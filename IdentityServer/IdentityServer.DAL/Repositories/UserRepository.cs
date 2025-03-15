using IdentityServer.DAL.Context.Interfaces;
using IdentityServer.DAL.Repositories.Interfaces;
using IdentityServer.Entities.ApplicationUser;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserContext _context;

        public UserRepository(IUserContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user,cancellationToken);
            await _context.Context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id,cancellationToken);
            
            _context.Users.Remove(user);
            await _context.Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email,cancellationToken);
            return user;
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id, cancellationToken);
            return user;
        }

        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.Context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
