using DaraAds.Application.Services.Abuse.Contracts;
using DaraAds.Application.Services.Abuse.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

//TODO выдача с пагинацией

namespace DaraAds.API.Controllers.Abuse
{
    public sealed partial class AbuseController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAbuseList(
            [FromQuery] GetAllRequest request,
            [FromServices] IAbuseService abuseService,
            CancellationToken cancellationToken)
        {
            return Ok(await abuseService.GetAbusePages(new GetAbusePages.Request
            {
                Limit = request.Limit,
                Offset = request.Offset
            }, cancellationToken));
        }
    }

    public sealed class GetAllRequest
    {
        /// <summary>
        /// Количество возвращаемых жалоб
        /// </summary>
        public int Limit { get; set; } = 10;

        /// <summary>
        /// Смещение с которого возвращаются жалобы
        /// </summary>
        public int Offset { get; set; } = 0;
    }
}