namespace DaraAds.Application.Services.Mail
{
    public static class MessageToChangeEmail
    {
        public static string Message(string callback)
        {
            var message = "Подтвердите новый Email, чтобы изменить его на DaraAds" +
                          $"<a href=\"{callback}\" class=\"btn\">Подтвердить изменение</a>";
            return message;
        }
    }
}