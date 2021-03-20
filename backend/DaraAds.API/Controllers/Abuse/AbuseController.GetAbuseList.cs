using DaraAds.Application.Services.Abuse.Contracts;
using DaraAds.Application.Services.Abuse.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;


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
            [FromQuery] GetAllRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await _abuseService.GetAbusePages(new GetAbusePages.Request
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