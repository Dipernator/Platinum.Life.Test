using Platinum.Life.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Life.Services
{
    public class PaymentRequisitionService
    {
        #region Define as Singleton
        private static PaymentRequisitionService _Instance;

        public static PaymentRequisitionService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PaymentRequisitionService();
                }

                return (_Instance);
            }
        }

        private PaymentRequisitionService()
        {
        }
        #endregion

        /// <summary>
        /// Create payment requisition
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<int> Create(Entities.PaymentRequisition model)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();
                // Default Payment requisition status to new
                model.StatusId = (int)PaymentRequisitionStatus.New;
                //model.Signature = new Signature();
                //model.DepartmentId = 1;
                //model.Attachment = new Attachment();
                //model.DateOfInvoice = DateTime.Now;
                context.PaymentRequisition.Add(model);

                int result = context.SaveChanges();

                return (result < 1) ? new Response<int>() { Entity = model.Id, Message = "", Success = false } : new Response<int>() { Entity = model.Id, Message = "", Success = true };
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return new Response<int>() { Entity = -1, Message = ex.Message, Success = false };
            }
        }

        /// <summary>
        /// Update payment requisition
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<int> Update(Entities.PaymentRequisition model)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                PaymentRequisition existingPaymentRequisition = context.PaymentRequisition.Find(model.Id);

                context.Entry(existingPaymentRequisition).CurrentValues.SetValues(model);
                context.Entry(existingPaymentRequisition.BankDetails).CurrentValues.SetValues(model.BankDetails);

                int result = context.SaveChanges();

                return (result < 1) ? new Response<int>() { Entity = result, Message = "", Success = false } : new Response<int>() { Entity = result, Message = "", Success = true };
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return new Response<int>() { Entity = -1, Message = ex.Message, Success = false };
            }
        }

        /// <summary>
        /// Get all payment requisition
        /// </summary>
        /// <returns></returns>
        public Response<List<PaymentRequisition>> GetAll()
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                List<PaymentRequisition> result = context.PaymentRequisition.ToList();

                return (result != null) ? new Response<List<PaymentRequisition>>() { Entity = result, Message = "", Success = true } : new Response<List<PaymentRequisition>>() { Entity = result, Message = "", Success = false };
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return new Response<List<PaymentRequisition>>() { Entity = null, Message = ex.Message, Success = false };
            }
        }

        /// <summary>
        /// Get all users payment requisitions
        /// </summary>
        /// <returns></returns>
        public Response<List<PaymentRequisition>> GetByUser(string userId)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                List<PaymentRequisition> result = context.PaymentRequisition.Where(m => m.UserId == userId).ToList();

                return (result != null) ? new Response<List<PaymentRequisition>>() { Entity = result, Message = "", Success = true } : new Response<List<PaymentRequisition>>() { Entity = result, Message = "", Success = false };
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return new Response<List<PaymentRequisition>>() { Entity = null, Message = ex.Message, Success = false };
            }
        }

        /// <summary>
        /// Get payment requisition by PaymentRequisitionId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response<PaymentRequisition> GetById(int id)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                PaymentRequisition result = context.PaymentRequisition.FirstOrDefault(m => m.Id == id);

                return (result != null) ? new Response<PaymentRequisition>() { Entity = result, Message = "", Success = true } : new Response<PaymentRequisition>() { Entity = result, Message = "", Success = false };
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return new Response<PaymentRequisition>() { Entity = null, Message = ex.Message, Success = false };
            }
        }

        /// <summary>
        /// Get all payment requisition by status
        /// </summary>
        /// <returns></returns>
        public Response<List<PaymentRequisition>> GetByStatus(PaymentRequisitionStatus status)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                List<PaymentRequisition> result = context.PaymentRequisition.Where(m => m.StatusId == (int)status).ToList();

                return (result != null) ? new Response<List<PaymentRequisition>>() { Entity = result, Message = "", Success = true } : new Response<List<PaymentRequisition>>() { Entity = result, Message = "", Success = false };
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return new Response<List<PaymentRequisition>>() { Entity = null, Message = ex.Message, Success = false };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Response<PaymentRequisition> UpdateStatus(string userId, int paymentRequisitionId, PaymentRequisitionStatus status)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                PaymentRequisition existingPaymentRequisition = context.PaymentRequisition.Find(paymentRequisitionId);
                PaymentRequisition modifiedPaymentRequisition = existingPaymentRequisition;
                modifiedPaymentRequisition.StatusId = (int)status;

                context.Entry(existingPaymentRequisition).CurrentValues.SetValues(modifiedPaymentRequisition);

                int result = context.SaveChanges();

                return (result < 1) ? new Response<PaymentRequisition>() { Entity = existingPaymentRequisition, Message = "", Success = false } : new Response<PaymentRequisition>() { Entity = modifiedPaymentRequisition, Message = "", Success = true };
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return new Response<PaymentRequisition>() { Entity = new PaymentRequisition(), Message = ex.Message, Success = false };
            }
        }

        /// <summary>
        /// Create attachment
        /// </summary>
        /// <param name="Attachment"></param>
        /// <returns></returns>
        public Response<int> CreateAttachment(Attachment model)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                PaymentRequisition existingPaymentRequisition = context.PaymentRequisition.Find(model.PaymentRequisitionId);
                int result = 0;
                if (existingPaymentRequisition != null) {
                    PaymentRequisition newPaymentRequisition = existingPaymentRequisition;
                    newPaymentRequisition.Attachment = model;
                    context.Entry(existingPaymentRequisition).CurrentValues.SetValues(newPaymentRequisition);

                    result  = context.SaveChanges();
                }

                return (result < 1) ? new Response<int>() { Entity = result, Message = "", Success = false } : new Response<int>() { Entity = result, Message = "", Success = true };
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return new Response<int>() { Entity = -1, Message = ex.Message, Success = false };
            }
        }

        /// <summary>
        /// Create attachment
        /// </summary>
        /// <param name="Signature"></param>
        /// <returns></returns>
        public Response<int> CreateSignature(Signature model)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                PaymentRequisition existingPaymentRequisition = context.PaymentRequisition.Find(model.PaymentRequisitionId);
                int result = 0;
                if (existingPaymentRequisition != null)
                {
                    PaymentRequisition newPaymentRequisition = existingPaymentRequisition;
                    newPaymentRequisition.Signature = model;
                    context.Entry(existingPaymentRequisition).CurrentValues.SetValues(newPaymentRequisition);

                    result = context.SaveChanges();
                }

                return (result < 1) ? new Response<int>() { Entity = result, Message = "", Success = false } : new Response<int>() { Entity = result, Message = "", Success = true };
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return new Response<int>() { Entity = -1, Message = ex.Message, Success = false };
            }
        }
    }
}
