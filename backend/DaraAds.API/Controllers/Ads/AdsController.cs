using DaraAds.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DaraAds.Application.Services.Ad.Interfaces;

namespace DaraAds.API.Controllers.Ads
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class AdsController : ControllerBase
    {
    }
}
