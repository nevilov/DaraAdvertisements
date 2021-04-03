using DaraAds.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Mail.Contracts.Exceptions
{
    public class SendingMailException : DomainException
    {
        public SendingMailException(string message) : base(message) { }
    }
}
