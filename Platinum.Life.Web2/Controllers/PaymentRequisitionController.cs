using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Platinum.Life.Entities;
using Platinum.Life.Services;
using Platinum.Life.Web2.Models;
using System;
using System.Collections;
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

        [Authorize]
        public ActionResult Dashboard()
        {
            Dashboard dashboard = new Dashboard();

            try
            {
                Response<List<PaymentRequisition>> paymentRequisitionResult = PaymentRequisitionService.Instance.GetAll();

                if (!paymentRequisitionResult.Success)
                {
                    return View(dashboard);
                }

                // New
                dashboard.TotalNew = paymentRequisitionResult.Entity.Where(m => m.StatusId == (int)PaymentRequisitionStatus.New).Count();
                // Approved
                dashboard.TotalApproved = paymentRequisitionResult.Entity.Where(m => m.StatusId == (int)PaymentRequisitionStatus.Approved).Count();
                // Total
                dashboard.Total = dashboard.TotalNew + dashboard.TotalApproved;

                return View(dashboard);
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return View(dashboard);
            }
        }

        // Get Payment Requisitions of logged in user
        [HttpGet]
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
                else
                {
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

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            string userId = User.Identity.Name;
            var user = UserManager.Users.Where(m => m.UserName == userId).FirstOrDefault();
            ViewBag.FirstName = user.FirstName;
            ViewBag.Surname = user.Surname;
            ViewBag.Email = user.Email;

            PaymentRequisitionViewModel model = new PaymentRequisitionViewModel
            {
                Department = DepartmentService.Instance.GetAll().Entity
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult CreateOrUpdate(PaymentRequisitionViewModel model)
        {
            try
            {
                model.UserId = User.Identity.GetUserId();

                // Create new Department
                if (model.DepartmentId == 0)
                {
                    Response<int> createDepartmentResult = DepartmentService.Instance.Create(new Department()
                    {
                        Name = model.DepartmentName
                    });

                    if (!createDepartmentResult.Success)
                    {
                        model.DepartmentId = 1; // Cheat code :(
                    }

                    model.DepartmentId = createDepartmentResult.Entity;
                }

                if (!ModelState.IsValid)
                {
                    string errorMessage = string.Join(" | ", ModelState.Values
                       .SelectMany(v => v.Errors)
                       .Select(e => e.ErrorMessage));
                    return Json(new { success = false, entity = "", message = errorMessage });
                }
                Response<int> createOrUpdatePaymentRequisitionResult = new Response<int>();
                PaymentRequisition paymentRequisition = new PaymentRequisition()
                {
                    Attachment = model.Attachment,
                    BankDetails = model.BankDetails,
                    CreateDate = model.CreateDate,
                    CreateDateTime = model.CreateDateTime,
                    DateOfInvoice = model.DateOfInvoice,
                    DepartmentId = model.DepartmentId,
                    Description = model.Description,
                    Id = model.Id,
                    ModifiedDateTime = model.ModifiedDateTime,
                    Signature = model.Signature,
                    StatusId = model.StatusId,
                    UserId = model.UserId
                };
                // Update
                if (model.Id > 0)
                {
                    createOrUpdatePaymentRequisitionResult = PaymentRequisitionService.Instance.Update(paymentRequisition);
                }
                // Create
                else
                {
                    createOrUpdatePaymentRequisitionResult = PaymentRequisitionService.Instance.Create(paymentRequisition);
                }

                return Json(new { success = createOrUpdatePaymentRequisitionResult.Success, entity = createOrUpdatePaymentRequisitionResult.Entity, message = createOrUpdatePaymentRequisitionResult.Message });
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return Json(new { success = false, entity = "", message = ex.ToString() });
            }
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            try
            {
                Response<PaymentRequisition> paymentRequisitionResult = PaymentRequisitionService.Instance.GetById(id);
                string userId = User.Identity.GetUserId();
                if (!paymentRequisitionResult.Success || paymentRequisitionResult == null)
                {
                    return View(new PaymentRequisitionViewModel());
                }

                // Check if PaymentRequisition has been sign. 
                if (paymentRequisitionResult.Entity.Signature == null)
                {
                    // Check if user have a saved Signature
                    Response<Signature> userSignature = SignatureService.Instance.GetByUserId(userId);
                    if (userSignature.Success)
                    {
                        paymentRequisitionResult.Entity.Signature = userSignature.Entity;
                    }
                }

                PaymentRequisitionViewModel paymentRequisitionViewModel = new PaymentRequisitionViewModel()
                {
                    Attachment = paymentRequisitionResult.Entity.Attachment,
                    BankDetails = paymentRequisitionResult.Entity.BankDetails,
                    CreateDate = paymentRequisitionResult.Entity.CreateDate,
                    CreateDateTime = paymentRequisitionResult.Entity.CreateDateTime,
                    DateOfInvoice = paymentRequisitionResult.Entity.DateOfInvoice,
                    DepartmentId = paymentRequisitionResult.Entity.DepartmentId,
                    Description = paymentRequisitionResult.Entity.Description,
                    Id = paymentRequisitionResult.Entity.Id,
                    ModifiedDateTime = paymentRequisitionResult.Entity.ModifiedDateTime,
                    Signature = paymentRequisitionResult.Entity.Signature,
                    StatusId = paymentRequisitionResult.Entity.StatusId,
                    UserId = paymentRequisitionResult.Entity.UserId,
                    DepartmentName = DepartmentService.Instance.GetById(paymentRequisitionResult.Entity.DepartmentId).Entity.Name
                };

                IQueryable<User> users = UserManager.Users;
                User user = users.Where(m => m.Id == paymentRequisitionViewModel.UserId).FirstOrDefault();
                paymentRequisitionViewModel.CreatedByName = $"{user.FirstName}, {user.Surname}";
                paymentRequisitionViewModel.CreatedByEmail = user.Email;


                // TODO
                if (paymentRequisitionViewModel.Signature != null)
                {
                    // if has different user signature
                    if (userId != paymentRequisitionViewModel.Signature.UserId)
                    {

                    }
                }

                ViewBag.UserId = userId;
                return View(paymentRequisitionViewModel);
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                // TODO : error page
                return View();
            }
        }

        // CLEAN UP
        [Authorize(Roles = "Admin")]
        public ActionResult SignOff(int paymentRequisitionId)
        {
            try
            {
                Response<PaymentRequisition> paymentRequisitionResult = PaymentRequisitionService.Instance.GetById(paymentRequisitionId);
                string userId = User.Identity.GetUserId();
                if (!paymentRequisitionResult.Success || paymentRequisitionResult == null)
                {
                    return View(new PaymentRequisitionViewModel());
                }

                // Check if PaymentRequisition has been sign. 
                if (paymentRequisitionResult.Entity.Signature == null)
                {
                    // Check if user have a saved Signature
                    Response<Signature> userSignature = SignatureService.Instance.GetByUserId(userId);
                    if (userSignature.Success)
                    {
                        paymentRequisitionResult.Entity.Signature = userSignature.Entity;
                    }
                }

                IQueryable<User> users = UserManager.Users;
                User user = users.Where(m => m.Id == userId).FirstOrDefault();

                var print = new PrintViewModel()
                {
                    Id = paymentRequisitionResult.Entity.Id.ToString(),
                    CreatedDate = paymentRequisitionResult.Entity.CreateDate.ToString(),
                    CreatedByEmail = user.Email,
                    CreatedByName = $"{user.FirstName}, {user.Surname}",
                    DepartmentName = DepartmentService.Instance.GetById(paymentRequisitionResult.Entity.DepartmentId).Entity.Name,
                    SignatureUrl = paymentRequisitionResult.Entity.Signature.Url,
                    BankDetail = $"{ paymentRequisitionResult.Entity.BankDetails.AccountHolder}, { paymentRequisitionResult.Entity.BankDetails.AccountNumber}, { paymentRequisitionResult.Entity.BankDetails.Bank}, { paymentRequisitionResult.Entity.BankDetails.Bank}",
                };

                var report = new Rotativa.ActionAsPdf("Print", print);

                return report;
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return View();
            }
        }

        public ActionResult Print(PrintViewModel model)
        {
            return View(model);
        }
    }
}