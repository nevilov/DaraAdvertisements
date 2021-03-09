using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.Mail
{
    public class MailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
