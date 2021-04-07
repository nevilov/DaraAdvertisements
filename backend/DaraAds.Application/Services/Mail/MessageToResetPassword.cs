using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Mail
{
    public static class MessageToResetPassword
    {
        public static string Message(string id, string encodedToken, string uri)
        {
            var message = $"Ссылка на восстановление пароля <a href=\"{uri}api/user/resetPassword?userId={id}&token={encodedToken}\" class=\"btn\">Восстановить пароль</a>";
            return message;
        }
    }
}
