using Platinum.Life.Entities;
using Platinum.Life.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Platinum.Life.Web2.Controllers
{
    public class UploadController : Controller
    {
        // GET: UploadFile
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadFile(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, int id, int attachmentType)
        {
            try
            {
                if (file == null) {
                    ViewBag.Message = "File upload failed!!";
                    return View(new { id, attachmentType });
                }
                if (file.ContentLength > 0)
                {
                    string fileExtension = System.IO.Path.GetExtension(file.FileName);
                    string fileName = $"{Guid.NewGuid()}{fileExtension}";

                    string _path = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                    file.SaveAs(_path);

                    Attachment attachment = new Attachment()
                    {
                        PaymentRequisitionId = id,
                        Url = fileName
                    };

                    PaymentRequisitionService.Instance.CreateAttachment(attachment);
                }
                ViewBag.Id = id;
                if (attachmentType == 1)
                {
                    ViewBag.MessageInvoice = "Supplier Invoice Uploaded Successfully!!";
                }
                if (attachmentType == 2)
                {
                    ViewBag.MessagePop = "Proof of Payment Uploaded Successfully!!";
                }

                return View(new { id, attachmentType });
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View(new { id, attachmentType });
            }
        }

        public void UploadSignature(string base64image, string userId, int paymentRequisitionId)
        {
            try
            {
                if (string.IsNullOrEmpty(base64image))
                    return;

                byte[] bytes = Convert.FromBase64String(base64image);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                string fileName = $"{Guid.NewGuid()}.png";
                string fullPath = Path.Combine(Server.MapPath("~/UploadedFiles/Signature/"), fileName);

                image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);

                // Save Signature
                Response<int> createSignatureResult = SignatureService.Instance.Create(new Signature()
                {
                    UserId = userId,
                    Url = fileName
                });

                if (createSignatureResult.Success)
                {
                    var asd = PaymentRequisitionService.Instance.CreateSignature(new Signature()
                    {
                        Url = fileName,
                        UserId = userId,
                        PaymentRequisitionId = paymentRequisitionId
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}