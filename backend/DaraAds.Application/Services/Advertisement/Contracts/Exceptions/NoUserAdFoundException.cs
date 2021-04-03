using DaraAds.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Advertisement.Contracts.Exceptions
{
    class NoUserAdFoundException : NotFoundException
    {
        public NoUserAdFoundException(string message) : base(message)
        {
        }
    }
}
