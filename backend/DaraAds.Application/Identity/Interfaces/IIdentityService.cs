using System;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Identity.Contracts;
using DaraAds.Application.Services.Mail.Contracts;

namespace DaraAds.Application.Identity.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetCurrentUserId(CancellationToken cancellationToken = default);

        Task<bool> IsInRole(string userId, string role, CancellationToken cancellationToken = default);

        Task<CreateUser.Response> CreateUser(CreateUser.Request request, CancellationToken cancellationToken = default);

        Task<CreateToken.Response> CreateToken(CreateToken.Request request, CancellationToken cancellationToken = default);

        Task<bool> ConfirmEmail(string userId, string token, CancellationToken cancellationToken = default);

        Task ChangeRole(ChangeRole.Request request, CancellationToken cancellationToken = default);

        Task SendEmailChangeToken(string newEmail, CancellationToken cancellationToken = default);

        Task<ConfirmChangeEmail.Response> ConfirmChangeEmail(ConfirmChangeEmail.Request request,
            CancellationToken cancellationToken = default);
        
        Task ChangePassword(ChangePassword.Request request, CancellationToken cancellationToken = default);

        Task<SendResetPasswordToken.Response> SendResetPasswordToken(SendResetPasswordToken.Request request, CancellationToken cancellationToken = default);

        Task<ResetUserPassword.Response> ResetPassword(ResetUserPassword.Request request, CancellationToken cancellationToken = default);

        Task<bool> BlockUser(string userId, DateTime untillDate, CancellationToken cancellationToken = default);

        Task SendEmailConfirmationToken(string userId, string email, CancellationToken cancellationToken = default);
    }
}
