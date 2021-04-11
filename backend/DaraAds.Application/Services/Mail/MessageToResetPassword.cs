using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Mail
{
    public static class MessageToResetPassword
    {
        public static string Message(string callback)
        {
            var message = $"Ссылка на восстановление пароля <a href=\"{callback}\" class=\"btn\">Восстановить пароль</a>";
            return message;
        }
    }
}
