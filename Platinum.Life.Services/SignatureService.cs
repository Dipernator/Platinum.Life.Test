using Platinum.Life.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Life.Services
{
    public class SignatureService
    {
        #region Define as Singleton
        private static SignatureService _Instance;

        public static SignatureService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SignatureService();
                }

                return (_Instance);
            }
        }

        private SignatureService()
        {
        }
        #endregion

        /// <summary>
        /// Create new Signature
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<int> Create(Entities.Signature model)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                context.Signature.Add(model);

                int result = context.SaveChanges();

                return (result < 1) ? new Response<int>() { Entity = result, Message = "", Success = false } : new Response<int>() { Entity = model.Id, Message = "", Success = true };
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return new Response<int>() { Entity = -1, Message = ex.Message, Success = false };
            }
        }

        /// <summary>
        /// Get Department by id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<Signature> GetByUserId(string userId)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                Signature result = context.Signature.FirstOrDefault(m => m.UserId == userId);

                return (result != null) ? new Response<Signature>() { Entity = result, Message = "", Success = true } : new Response<Signature>() { Entity = result, Message = "", Success = false };
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return new Response<Signature>() { Entity = null, Message = ex.Message, Success = false };
            }
        }
    }
}
