using DaraAds.Application.Services.Abuse.Contracts;
using DaraAds.Application.Services.Abuse.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Abuse
{
    public sealed partial class AbuseController : ControllerBase
    {
        /// <summary>
        /// Создать жалобу на объявление. (Пользователь)
        /// </summary>
        /// <param name="abuseBinding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateAbuse(
            [FromBody] CreateAbuseBinding abuseBinding,
            CancellationToken cancellationToken)
        {
            var response = await _abuseService.CreateAbuse(new CreateAbuse.Request
            {
                AbuseAdvId = abuseBinding.AdvId,
                AbuseText = abuseBinding.AbuseText,
            }, cancellationToken);;
            return Created($"api/v1/abuse/{response.Id}", new { });
        }
    }
    public sealed class CreateAbuseBinding
    {
        public int AdvId { get; set; }
        [Required, MinLength(5), MaxLength(300)]
        public string AbuseText { get; set; }
    }
}
