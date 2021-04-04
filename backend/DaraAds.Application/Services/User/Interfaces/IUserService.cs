using DaraAds.Application.Services.User.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.User.Interfaces
{
    public interface IUserService
    {
        Task<Register.Response> Register(Register.Request request, CancellationToken cancellationToken);

        Task Update(Update.Request request, CancellationToken cancellationToken);
        
        Task AddImage(AddImage.Request request, CancellationToken cancellationToken);
        Task DeleteImage(DeleteImage.Request request, CancellationToken cancellationToken);

    } 
}