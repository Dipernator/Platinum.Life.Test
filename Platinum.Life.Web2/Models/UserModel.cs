using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Platinum.Life.Web2.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserModel : UserModel
    {
        public string ConfirmPassword { get; set; }
    }

    public class LoginUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}