using Platinum.Life.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Life.Services
{
    public class CommunicationsService
    {
        #region Define as Singleton
        private static CommunicationsService _Instance;

        public static CommunicationsService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CommunicationsService();
                }

                return (_Instance);
            }
        }

        private CommunicationsService()
        {
        }
        #endregion

        public void SentEmail(Email email)
        {

        }
    }
}
