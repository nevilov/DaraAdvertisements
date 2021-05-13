using DaraAds.Application.Services.Abuse.Contracts;
using DaraAds.Application.Services.Advertisement.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Abuse.Interfaces
{
    public interface IAbuseService
    {
        /// <summary>
        /// Создать жалобу объявления
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CreateAbuse.Response> CreateAbuse(CreateAbuse.Request request, CancellationToken cancellationToken);
        
        /// <summary>
        /// Получить жалобы
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<GetAbusePages.Response> GetAbusePages(GetAbusePages.Request request, CancellationToken cancellationToken);
       
        /// <summary>
        /// Закрыть объявления
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CloseAbuse(CloseAbuse.Request request, CancellationToken cancellationToken);
    }
}
