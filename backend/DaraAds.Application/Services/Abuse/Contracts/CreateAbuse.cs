using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Abuse.Contracts
{
    public static class CreateAbuse
    {
        public sealed class Request
        {
            public int AbuseAdvId { get; set; }
            public string AbuseText { get; set; }
        }

        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}
