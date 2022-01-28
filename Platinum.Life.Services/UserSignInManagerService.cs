using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Platinum.Life.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Life.Services
{
    public class UserSignInManagerService : SignInManager<User, string>
    {
        public UserSignInManagerService(UserManagerService userManager, IAuthenticationManager authenticationManager)
           : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserManagerService)UserManager);
        }

        public static UserSignInManagerService Create(IdentityFactoryOptions<UserSignInManagerService> options, IOwinContext context)
        {
            return new UserSignInManagerService(context.GetUserManager<UserManagerService>(), context.Authentication);
        }
    }
}
