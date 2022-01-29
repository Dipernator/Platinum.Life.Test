using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Platinum.Life.Entities;
using Platinum.Life.Services;
using Platinum.Life.Web2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Platinum.Life.Web2.Controllers
{
    public class UserController : Controller
    {
        private UserSignInManagerService _signInManager;
        private UserManagerService _userManager;

        public UserSignInManagerService SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<UserSignInManagerService>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public UserManagerService UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManagerService>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new LoginUserModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Login(LoginUserModel model)
        {
            try
            {
                SignInStatus signInStatusResult = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

                return Json(new { success = signInStatusResult, entity = "", message = "Invalid login attempt" });

                //switch (signInStatusResult)
                //{
                //    case SignInStatus.Success:
                //        return Json(new { success = signInStatusResult, entity = "", message = "" });
                //    case SignInStatus.Failure:
                //        return Json(new { success = signInStatusResult, entity = "", message = "" });
                //    default:
                //        return Json(new { success = signInStatusResult, entity = "", message = "" });
                //}
            }
            catch (Exception ex)
            {
                return Json(new { success = false, entity = "", message = ex.ToString() });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [OutputCache(Duration = 0)]
        public ActionResult Register()
        {
            return View(new RegisterUserModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Register(RegisterUserModel model)
        {
            try
            {
                if (model.Password != model.ConfirmPassword)
                {
                    return Json(new { success = false, entity = "", message = "Password missmatch" });
                }

                User user = new User { FirstName = model.FirstName, Surname = model.Surname, UserName = model.Email, Email = model.Email };
                IdentityResult createUserResult = await UserManager.CreateAsync(user, model.Password);

                if (!createUserResult.Succeeded)
                {
                    return Json(new { success = createUserResult.Succeeded, entity = createUserResult.Succeeded, message = string.Join(" ", createUserResult.Errors) });
                }

                return Json(new { success = createUserResult.Succeeded, entity = createUserResult.Succeeded, message = createUserResult.Succeeded });


                // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, entity = "", message = "" });
            }
        }
    }
}