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
                Response<List<PaymentRequisition>> paymentRequisitionResult = new Response<List<PaymentRequisition>>();

                if (User.IsInRole("Admin"))
                {
                    paymentRequisitionResult = PaymentRequisitionService.Instance.GetAll();
                }
                else {
                    paymentRequisitionResult = PaymentRequisitionService.Instance.GetByUser(User.Identity.GetUserId());
                }

                if (!paymentRequisitionResult.Success)
                {
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateOrUpdate(PaymentRequisition model)
        {
            try
            {
                Response<int> createOrUpdatePaymentRequisitionResult = new Response<int>();
                model.UserId = User.Identity.GetUserId();
                // Update
                if (model.Id > 0)
                {
                    createOrUpdatePaymentRequisitionResult = PaymentRequisitionService.Instance.Update(model);
                }
                // Create
                else
                {    
                    createOrUpdatePaymentRequisitionResult = PaymentRequisitionService.Instance.Create(model);
                }

                return Json(new { success = createOrUpdatePaymentRequisitionResult.Success, entity = createOrUpdatePaymentRequisitionResult.Entity, message = createOrUpdatePaymentRequisitionResult.Message });
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return Json(new { success = false, entity = "", message = ex.ToString() });
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details(int id)
        {
            try
            {
            
                Response<PaymentRequisition> paymentRequisitionResult = PaymentRequisitionService.Instance.GetById(id);
                if (!paymentRequisitionResult.Success || paymentRequisitionResult == null)
                {
                    return View(new Response<PaymentRequisition>());
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
    }
}