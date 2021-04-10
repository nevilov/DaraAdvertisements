using DaraAds.Application.Services.User.Contracts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Favorite.Contracts.Exceptions
{
    public class DuplicateFavoriteException : DuplicateException
    {
        public DuplicateFavoriteException(string message) : base(message)
        {
        }
    }
}
