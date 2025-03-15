using IdentityServer.DAL.Configurations.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.DAL.Configurations
{
    public static class Configuration
    {
        public static ModelBuilder AddIdentityConfiguration(this ModelBuilder modelBuilder) 
        {
            modelBuilder
                   .ApplyConfiguration(new UserConfiguration());

            return modelBuilder;
        }
    }
}
