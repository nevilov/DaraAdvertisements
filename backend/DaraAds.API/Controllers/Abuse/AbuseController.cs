using DaraAds.Application.Services.Abuse.Contracts;
using DaraAds.Application.Services.Abuse.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Abuse
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public sealed partial class AbuseController : ControllerBase
    {
        public sealed class CreateAbuseBinding
        {
            [Required]
            public int AbuseAdvId { get; set; }
            [Required, MinLength(5), MaxLength(300)]
            public string AbuseText { get; set; }
        }
 

    }
}
