using DaraAds.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Advertisement.Contracts.Exceptions
{
    public class NoUserFound : NotFoundException
    {
        public NoUserFound(string message) : base(message)
        {
        }
    }
}
