using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Platinum.Life.Entities;
using Platinum.Life.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Platinum.Life.Web2.Controllers
{
    public class PaymentRequisitionController : Controller
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

        public PaymentRequisitionController()
        {
        }

        public PaymentRequisitionController(UserManagerService userManager, UserSignInManagerService signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        // GET: PaymentRequisition
        public ActionResult Index()
        {
            PaymentRequisitionService.Instance.GetAll();

            return View();
        }

        // Get Payment Requisitions of logged in user
        [Authorize]
        public ActionResult List()
        {
            try
            {
                Response<List<PaymentRequisition>> paymentRequisitionResult = PaymentRequisitionService.Instance.GetByUser(User.Identity.GetUserId());

                if (!paymentRequisitionResult.Success) {
                    return View(new List<PaymentRequisition>());
                }

                return View(paymentRequisitionResult.Entity);
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                // TODO : error page
                return View();
            }
        }


        public ActionResult Create()
        {
            return View(new PaymentRequisition());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(PaymentRequisition model) {
            try
            {
                //BankDetails = new 
                //PaymentRequisition paymentRequisition = new PaymentRequisition()
                //{
                //    BankDetails = 

                //};
                model.UserId = User.Identity.GetUserId();

                Response<int> createPaymentRequisitionResult =  PaymentRequisitionService.Instance.Create(model);

                return Json(new { success = true, entity = "", message = "" });
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return Json(new { success = false, entity = "", message = ex.ToString() });
            }
        }
    }
}