using DaraAds.Application.Services.Abuse.Contracts;
using DaraAds.Application.Services.Abuse.Interfaces;
using DaraAds.Application.Services.User.Contracts.Extantions;
using DaraAds.Application.Services.User.Interfaces;
using DaraAds.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Abuse.Implementations
{
    public sealed class AbuseServiceV1 : IAbuseService
    {
        private readonly IRepository<Domain.Abuse, int> _repository;
        private readonly IUserService _userService;

        public AbuseServiceV1(IRepository<Domain.Abuse, int> repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<CreateAbuse.Response> CreateAbuse(
            CreateAbuse.Request request,
            CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrent(cancellationToken);
            if (user == null)
            {
                throw new NoRightsException("Нет прав");
            }


            var abuse = new Domain.Abuse
            {
                AbuseAdvId = request.AbuseAdvId,
                AbuseText = request.AbuseText,
                AuthorId = user.Id,
                CreatedDate = DateTime.UtcNow
            };
            await _repository.Save(abuse, cancellationToken);

            return new CreateAbuse.Response
            {
                Id = abuse.Id
            };
        }


        public async Task<GetAbusePages.Response> GetAbusePages(GetAbusePages.Request request, CancellationToken cancellationToken)
        {

            var total = await _repository.Count(cancellationToken);
            if (total == 0)
            {
                return new GetAbusePages.Response
                {
                    Total = 0,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }

            var abuses = await _repository.GetPaged(request.Offset, request.Limit, cancellationToken);

            return new GetAbusePages.Response
            {
                Items = abuses.Select(a => new GetAbusePages.Response.Item
                {
                    Id = a.Id,
                    AuthorId = a.AuthorId,
                    AbuseAdvId = a.AbuseAdvId,
                    Priority = a.Priority,
                    AbuseText = a.AbuseText
                }),
                Total = 10,
                Offset = request.Offset,
                Limit = request.Limit
            };
        }
    }
}
