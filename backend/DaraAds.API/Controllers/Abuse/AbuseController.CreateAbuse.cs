using DaraAds.Application.Services.Abuse.Contracts;
using DaraAds.Application.Services.Abuse.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Abuse
{
    public sealed partial class AbuseController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAbuse(
            [FromBody] CreateAbuseBinding abuseBinding,
            [FromServices] IAbuseService abuseService,
            CancellationToken cancellationToken)
        {
            var response = await abuseService.CreateAbuse(new CreateAbuse.Request
            {
                AbuseText = abuseBinding.AbuseText,
                AbuseAdvId = abuseBinding.AbuseAdvId
            }, cancellationToken);
            return Created($"api/v1/abuse/{0}", new { });
        }
    }
}
