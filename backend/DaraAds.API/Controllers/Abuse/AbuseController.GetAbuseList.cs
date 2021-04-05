using DaraAds.Application.Services.Abuse.Contracts;
using DaraAds.Application.Services.Abuse.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.API.Dto.Abuse;


namespace DaraAds.API.Controllers.Abuse
{
    public sealed partial class AbuseController : ControllerBase
    {
        /// <summary>
        /// Получить все объявления. (Модератор)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public async Task<IActionResult> GetAbuseList(
            [FromQuery] GetAllAbuseRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await _abuseService.GetAbusePages(new GetAbusePages.Request
            {
                Limit = request.Limit,
                Offset = request.Offset
            }, cancellationToken));
        }
    }
}