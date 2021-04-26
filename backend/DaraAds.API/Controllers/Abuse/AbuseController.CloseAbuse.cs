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
using DaraAds.API.Dto.Abuse;
using DaraAds.Application.Services.Advertisement.Contracts;

namespace DaraAds.API.Controllers.Abuse
{
    public sealed partial class AbuseController : ControllerBase
    {
        /// <summary>
        /// Закрыть (рассмотреть) жалобу. (Модератор)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        

        [Authorize(Roles = "Moderator")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken
            )
        {
            await _abuseService.CloseAbuse(new CloseAbuse.Request
            {
                Id = id
            }, cancellationToken);
            return NoContent();
        }

    }

}
