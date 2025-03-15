using IdentityServer.DAL.Context.Interfaces;
using IdentityServer.Entities.ApplicationUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.DAL.Context
{
    public class ApplicationDbContext : DbContext, IDbContextProvider, IUserContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbContext Context => this;

        public DbSet<User> Users { get; set; }
    }
}
