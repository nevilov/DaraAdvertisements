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
using DaraAds.Application.Common;
using DaraAds.Application.Identity.Contracts;
using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Services.Mail.Exception;
using DaraAds.Application.Services.Mail.Interfaces;
using DaraAds.Application.Services.User.Contracts.Extantions;
using DaraAds.Domain;
using DaraAds.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public IdentityService(UserManager<IdentityUser> userManager, 
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            IMailService mailService)
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

                var confiramtionToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var encodedToken = HttpUtility.UrlEncode(confiramtionToken);

                var message = $"<a href=\"{_configuration["ApiUri"]}api/v1/users/confirm?userId={newUser.Id}&token={encodedToken}\">Подтвердить email</a>";

                try
                {
                    await _mailService.Send(request.Email, "Подтвердите Email!", message, cancellationToken);
                }
                catch(Exception ex)
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

            var passworkCheck =  await _userManager.CheckPasswordAsync(identityUser, request.Password);
            if (!passworkCheck)
            {
                throw new NoRightsException("Неправильный логин или пароль");
            }

            var claims = new List<Claim>
             {
                 new(ClaimTypes.Email, request.Email),
                 new(ClaimTypes.NameIdentifier, identityUser.Id)
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
            if(user == null)
            {
                throw new NoUserFoundException("Пользователь не найден");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded;
        }
    }
}