using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Advertisement.Contracts;
using Microsoft.AspNetCore.Http;

namespace DaraAds.Application.Services.Advertisement.Interfaces
{
    public interface IAdvertisementService
    {
        /// <summary>
        /// Создать объявление
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken);

        /// <summary>
        /// Получить объявление по Id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Get.Response> Get(Get.Request request, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить объявление
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Delete(Delete.Request request, CancellationToken cancellationToken);

        /// <summary>
        /// Обновить объявление
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken);
        
        /// <summary>
        /// Получить объявления
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<GetPages.Response> GetPages(GetPages.Request request, CancellationToken cancellationToken);

        /// <summary>
        /// Получить объявления пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<GetUserAdvertisements.Response> GetUserAdvertisements(GetUserAdvertisements.Request request,CancellationToken cancellationToken);
        
        /// <summary>
        /// Добавить картинку объявлению
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddImage(AddImage.Request request, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить объявления
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteImage(DeleteImage.Request request, CancellationToken cancellationToken);

        /// <summary>
        /// Массовая загрузка объявлений
        /// </summary>
        /// <param name="excel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ImportExcelProducer(IFormFile excel, CancellationToken cancellationToken);

        /// <summary>
        /// Создание объявлений консюмером
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CreateByExcelConsumer(ImportExcelMessage message, CancellationToken cancellationToken);

    }
}