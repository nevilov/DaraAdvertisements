namespace DaraAds.Application.Services.Mail
{
    public static class MessageToConfirmEmail
    {
        public static string Message(string id, string email, string encodedToken, string uri)
        {
            var message = @"<html lang='en'><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><title>DaraAds Mail</title><style>@import url('https://fonts.googleapis.com/css2?family=Inter:wght@100;200;3..');* {margin: 0;padding: 0;font-family: 'Inter', sans-serif;}body, html {height: 100%;width: 100%;display: flex;justify-content: center;align-items: center;}.logo {max-width: 300px;margin-bottom: 40px;align-self: center;}.content {width: 800px;display: flex;justify-content: center;flex-direction: column;padding: 30px 50px;box-sizing: border-box;}.content_mainInfo {font-size: 20px;margin-bottom: 20px;line-height: 150%;}.content_mainFeachuresList, .content_mainFeachures {font-size: 20px;}.content_slogan {font-size: 20px;margin-bottom: 20px;align-self: center;}.content_footer {font-size: 16px;text-align: center;align-self: center;}ul {margin-left: 30px;}ul li {margin-top: 10px;}.btn {margin: 40px 0;background: #23C4D6;padding: 15px 35px;font-size: 18px;font-weight: 500;text-decoration: none;color: #fff;max-width: 300px;align-self: center;border-radius: 4px;}</style></head><body><div class='content'><img class='logo' src='https://c.radikal.ru/c04/2103/47/a6199ead689d.png' /><p class='content_mainInfo'>Вы успешно зарегистрировались на <strong>DaraAds</strong> и присоединились к самой большой платформе показа объявлений в Севастополе. Теперь Вам необходимо <strong>подтвердить</strong> Вашу <strong>электронную почту</strong>, сделать это можно кликнув по кнопке ниже.</p><p class='content_mainFeachures'>После потверждения Вам будут доступны:</p><ul class='content_mainFeachuresList'><li>простр объявлений</li><li>размещение своих объявлений</li></ul>"
+ $"<a href=\"{uri}api/user/confirm?userId={id}&token={encodedToken}\" class=\"btn\">Подтвердить мой email</a>"
+ "<p class='content_slogan'>DaraAds - все для быстрых продаж и<br>комфортного поиска необходимого!</p><p class='content_footer'>С уважением, <br> служба поддержки DaraAds</p></div></body></html>"
+ $"Если по какой-то причине произошла ошибка, вышлите себе токен подтверждения еще раз, нажав <a href=\"{uri}api/user/send/confirmEmailToken?userId={id}&email={email}\">сюда</a>";
            
            return message;
        }
    }
}

