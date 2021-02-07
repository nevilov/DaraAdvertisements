using DaraAds.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DaraAds.API.Controllers.Users;
using DaraAds.Application.Services.Ad.Interfaces;
using System.Threading;
using DaraAds.Application.Services.Ad.Contracts;

namespace DaraAds.API.Controllers.Ads
{
    public partial class AdsController : ControllerBase
    {
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            [FromServices] IAdService service,
            CancellationToken cancellationToken
            )
        {
            await service.Delete(new Delete.Request
            {
                Id = id
            }, cancellationToken);
            return NoContent();
        }
    }
}