using DaraAds.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Identity.Contracts.Exceptions
{
    public class RoleException : DomainException
    {
        public RoleException(string message) : base(message)
        {
        }
    }
}
