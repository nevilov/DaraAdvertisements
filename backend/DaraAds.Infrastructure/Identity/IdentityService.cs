using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Advertisement.Application.Identity.Contracts.Exceptions;
using DaraAds.Application.Identity.Contracts;
using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Services.Mail.Contracts.Exceptions;
using DaraAds.Application.Services.Mail.Interfaces;
using DaraAds.Application.Services.User.Contracts.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DaraAds.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;

        public IdentityService(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IMailService mailService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _mailService = mailService;
        }
        public Task<string> GetCurrentUserId(CancellationToken cancellationToken = default)
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
            return Task.FromResult(_userManager.GetUserId(claimsPrincipal));
        }

        public async Task<bool> IsInRole(string userId, string role, CancellationToken cancellationToken = default)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            if (identityUser == null)
            {
                throw new IdentityUserNotFoundException("Пользователь не найден");
            }

            return await _userManager.IsInRoleAsync(identityUser, role);
        }

        public async Task<CreateUser.Response> CreateUser(CreateUser.Request request, CancellationToken cancellationToken = default)
        {
            var existedUser = await _userManager.FindByEmailAsync(request.Email);
            if (existedUser != null)
            {
                throw new DuplicateException("Пользователь с таким именем уже существует");
            }

            var newUser = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Username
            };
            var identityResult = await _userManager.CreateAsync(newUser, request.Password);
            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, request.Role);

                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var encodedToken = HttpUtility.UrlEncode(confirmationToken);
                var message = @"<html lang='en'><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><title>DaraAds Mail</title><style>@import url('https://fonts.googleapis.com/css2?family=Inter:wght@1..);* {margin: 0;padding: 0;font-family: 'Inter', sans-serif;}body, html {height: 100%;width: 100%;display: flex;justify-content: center;align-items: center;}.logo {max-width: 300px;margin-bottom: 40px;align-self: center;}.content {width: 800px;display: flex;justify-content: center;flex-direction: column;padding: 30px 50px;box-sizing: border-box;}.content_mainInfo {font-size: 20px;margin-bottom: 20px;line-height: 150%;}.content_mainFeachuresList, .content_mainFeachures {font-size: 20px;}.content_slogan {font-size: 20px;margin-bottom: 20px;align-self: center;}.content_footer {font-size: 16px;text-align: center;align-self: center;}ul {margin-left: 30px;}ul li {margin-top: 10px;}.btn {margin: 40px 0;background: #23C4D6;padding: 15px 35px;font-size: 18px;font-weight: 500;text-decoration: none;color: #fff;max-width: 300px;align-self: center;border-radius: 4px;}</style></head><body><div class='content'><img class='logo' src='https://c.radikal.ru/c04/2103/47/a6199ead689d.png' /><p class='content_mainInfo'>Вы успешно зарегистрировались на <strong>DaraAds</strong> и присоединились к самой большой платформе показа объявлений в Севастополе. Теперь Вам необходимо <strong>подтвердить</strong> Вашу <strong>электронную почту</strong>, сделать это можно кликнув по кнопке ниже.</p><p class='content_mainFeachures'>После потверждения Вам будут доступны:</p><ul class='content_mainFeachuresList'><li>простр объявлений</li><li>размещение своих объявлений</li></ul>"
+ $"<a href=\"{_configuration["ApiUri"]}api/user/confirm?userId={newUser.Id}&token={encodedToken}\" class=\"btn\">Подтвердить мой email</a>"
+ "<p class='content_slogan'>DaraAds - все для быстрых продаж и<br>комфортного поиска необходимого!</p><p class='content_footer'>С уважением, <br> служба поддержки DaraAds</p></div></body></html>";
                try
                {
                    await _mailService.Send(request.Email, "Подтвердите Email!", message, cancellationToken);
                }
                catch (Exception ex)
                {
                    await _userManager.DeleteAsync(newUser);
                    throw new SendingMailException("Произошла ошибка!" + ex.Message);
                }

                return new CreateUser.Response
                {
                    IsSuccess = true,
                    UserId = newUser.Id
                };
            }

            return new CreateUser.Response
            {
                IsSuccess = false,
                Errors = identityResult.Errors.Select(x => x.Description).ToArray()
            };
        }

        public async Task<CreateToken.Response> CreateToken(CreateToken.Request request, CancellationToken cancellationToken = default)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Email);
            if (identityUser == null)
            {
                throw new IdentityUserNotFoundException("Пользователь не найден");
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(identityUser, request.Password);
            if (!passwordCheck)
            {
                throw new NoRightsException("Неправильный логин или пароль");
            }

            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(identityUser);
            if (!isEmailConfirmed)
            {
                throw new NoRightsException("Подтвердите почту, чтобы войти!");
            }

            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Email, request.Email),
                 new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
             };

            var userRoles = await _userManager.GetRolesAsync(identityUser);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                    SecurityAlgorithms.HmacSha256
                )
            );
            return new CreateToken.Response
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<bool> ConfirmEmail(string userId, string token, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new IdentityUserNotFoundException("Пользователь не найден");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }
    }
}