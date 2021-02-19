using DaraAds.Application.Services.User.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.Identity
{
    public class HttpContextClaimsAccessor : IClaimsAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public HttpContextClaimsAccessor(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;
        public async Task<IEnumerable<Claim>> GetCurrentClaims(CancellationToken cancellationToken) => _contextAccessor.HttpContext.User.Claims;
    }
}
