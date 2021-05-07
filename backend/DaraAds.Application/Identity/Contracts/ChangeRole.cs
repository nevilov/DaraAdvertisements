using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Identity.Contracts
{
    public static class ChangeRole
    {
        public class Request
        {
            public string UserId;

            public string NewRole;
        }
    }
}
