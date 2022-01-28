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
                SignInStatus result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        return Json(new { success = true, entity = "", message = "" });
                    case SignInStatus.Failure:
                        return Json(new { success = false, entity = "", message = "" });
                    default:
                        return Json(new { success = false, entity = "", message = "" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, entity = "", message = ex.ToString()});
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
            JsonResult jsonResult = new JsonResult();
            try
            {

                var user = new User { FirstName = "Tshepang", Surname = "Motloung", UserName = "tamotloung@hotmail.co.za", Email = "tamotloung@hotmail.co.za" };
                var result = await UserManager.CreateAsync(user, "ts4life@ONEADMIN");

                jsonResult.Data = new { Success = true, Message = "true" };
            }
            catch (Exception ex)
            {
                jsonResult.Data = new { Success = false, Message = ex.Message };
            }

            return jsonResult;
        }
    }
}