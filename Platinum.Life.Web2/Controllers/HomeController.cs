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
            //PaymentRequisition paymentRequisition = new PaymentRequisition()
            //{
            //    CreateDate = DateTime.Now,
            //    DepartmentId = 1,
            //    DateOfInvoice = DateTime.Now,
            //    Description = "Description Celetesss",
            //    UserId = "b7f9efff-fc6d-4bec-89ce-6312e9799221"
            //};

            //BankDetails bankDetails = new BankDetails()
            //{
            //    AccountHolder = "Celete",
            //    AccountNumber = "9999999999",
            //    Bank = "FNB",
            //    BranchCode = "0000"
            //};

            //paymentRequisition.BankDetails = bankDetails;

            //var res = PaymentRequisitionService.Instance.Add(paymentRequisition);


            //var reasdasds = PaymentRequisitionService.Instance.GetById(5);

  
            return View();
        }

        public async Task<JsonResult> Login()
        {

            //var user = await UserManager.FindByNameAsync("test");
            JsonResult jsonResult = new JsonResult();
            try
            {
                PasswordHasher passwordHasher = new PasswordHasher();
                var p = passwordHasher.HashPassword("kjbasjdbasdnaskdasdjsa");
                var result = await SignInManager.PasswordSignInAsync("test@test.com", p, false, shouldLockout: false);


                switch (result)
                {
                    case SignInStatus.Success:

                        jsonResult.Data = new { Success = true, RequiresVerification = false, JsonRequestBehavior.AllowGet };
                        break;
                    case SignInStatus.RequiresVerification:
                        jsonResult.Data = new { Success = true, RequiresVerification = true, JsonRequestBehavior.AllowGet };
                        break;
                    case SignInStatus.LockedOut:
                        jsonResult.Data = new { Success = false, Messages = "LockedOut", JsonRequestBehavior.AllowGet };
                        break;
                    case SignInStatus.Failure:
                        jsonResult.Data = new { Success = false, Messages = "LockedOut", JsonRequestBehavior.AllowGet };
                        break;
                    default:
                        jsonResult.Data = new { Success = false, Messages = "nvalidLoginAttempt", JsonRequestBehavior.AllowGet };
                        break;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Your application description page.";
            }


            return jsonResult;
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