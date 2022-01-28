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

        public Response<int> Add(Entities.Department model)
        {
            try
            {
                Data.DbContext context = new Data.DbContext();

                context.Department.Add(model);

                int result = context.SaveChanges();

                return (result < 1) ? new Response<int>() { Entity = result, Message = "", Success = true } : new Response<int>() { Entity = result, Message = "", Success = false };
            }
            catch (Exception ex)
            {
                return new Response<int>() { Entity = -1, Message = ex.Message, Success = false };
            }
        }
    }
}
