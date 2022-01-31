using Platinum.Life.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Life.Services
{
    public class DepartmentService
    {
        #region Define as Singleton
        private static DepartmentService _Instance;

        public static DepartmentService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DepartmentService();
                }

                return (_Instance);
            }
        }

        private DepartmentService()
        {
        }
        #endregion

        /// <summary>
        /// Create new Department
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<int> Create(Entities.Department model)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                context.Department.Add(model);

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
        public Response<Department> GetById(int id)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                Department result = context.Department.FirstOrDefault(m => m.Id == id);

                if (result == null) {
                    result = new Department();
                }

                return (result != null) ? new Response<Department>() { Entity = result, Message = "", Success = true } : new Response<Department>() { Entity = result, Message = "", Success = false };
            }
            catch (Exception ex)
            {
                LoggingService.Instance.LogException(ex);
                return new Response<Department>() { Entity = new Department(), Message = ex.Message, Success = false };
            }
        }
    }
}
