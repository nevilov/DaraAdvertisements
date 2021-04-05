using DaraAds.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.User.Contracts.Exceptions
{
    public sealed class NoUserFoundException : NotFoundException
    {
        public NoUserFoundException(string message) : base(message) { }
    }
}
