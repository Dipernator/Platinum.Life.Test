using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Life.Services
{
    public class LoggingService
    {
        #region Define as Singleton
        private static LoggingService _Instance;

        public static LoggingService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new LoggingService();
                }

                return (_Instance);
            }
        }

        private LoggingService()
        {
        }
        #endregion

        public void LogException(Exception ex) { 
        
        }
    }
}
