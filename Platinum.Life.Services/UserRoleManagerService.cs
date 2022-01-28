using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Life.Services
{
    public class UserRoleManagerService : RoleManager<IdentityRole>
    {
        public UserRoleManagerService(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {
        }

        public static UserRoleManagerService Create(IdentityFactoryOptions<UserRoleManagerService> options, IOwinContext context)
        {
            return new UserRoleManagerService(new RoleStore<IdentityRole>(context.Get<DbContext>()));
        }
    }
}
