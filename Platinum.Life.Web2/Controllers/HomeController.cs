using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Platinum.Life.Data;
using Platinum.Life.Entities;
using Platinum.Life.Services;
using Platinum.Life.Web2.Models;

namespace Platinum.Life.Web2.Controllers
{
    public class HomeController : Controller
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

        public HomeController()
        {
        }

        public HomeController(UserManagerService userManager, UserSignInManagerService signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ActionResult Index()
        {
            return View();
            //// Check user role
            ////var q = User.Identity.IsAuthenticated;
            ////var s = User.IsInRole("Admin");
            ////var a = User.Identity.GetUserId();

            //if (User.IsInRole("Admin")) {
            //    return View();
            //}
            //return RedirectToAction("Index", "PaymentRequisition");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}