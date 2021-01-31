using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Core.Entities
{
    /// <summary>
    /// Сущность пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public int Name { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public int LastName { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public int Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public int Password { get; set; }
    }
}
