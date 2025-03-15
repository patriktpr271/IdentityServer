using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Entities.Dtos
{
    public class CreateUserDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        
    }
}
