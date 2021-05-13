using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Abuse.Contracts;
using DaraAds.Application.Services.Abuse.Interfaces;
using DaraAds.Application.Services.User.Contracts.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Contracts.Exceptions;

namespace DaraAds.Application.Services.Abuse.Implementations
{
    public sealed class AbuseService : IAbuseService
    {
        private readonly IRepository<Domain.Abuse, int> _repository;
        private readonly IIdentityService _identityService;

        public AbuseService(IRepository<Domain.Abuse, int> repository, IIdentityService identityService)
        {
            _repository = repository;
            _identityService = identityService;
        }

        public async Task CloseAbuse(CloseAbuse.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new NoUserFoundException($"Нет прав");
            }

            var abuse = await _repository.FindById(request.Id, cancellationToken);

            if (abuse == null)
            {
                throw new AbuseNotFoundException(request.Id);
            }

            abuse.RemovedDate = DateTime.UtcNow;

            await _repository.Save(abuse, cancellationToken);
        }

        public async Task<CreateAbuse.Response> CreateAbuse(
            CreateAbuse.Request request,
            CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            if (userId == null)
            {
                throw new NoRightsException("Нет прав");
            }


            var abuse = new Domain.Abuse
            {
                AbuseAdvId = request.AbuseAdvId,
                AbuseText = request.AbuseText,
                AuthorId = userId,
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

            var abuses = await _repository.GetPaged(a => a.RemovedDate == null, request.Offset, request.Limit, cancellationToken);
            total =  await _repository.Count(a => a.RemovedDate == null, cancellationToken);
            return new GetAbusePages.Response
            {
                Items = abuses.Select(a => new GetAbusePages.Response.Item
                {
                    Id = a.Id,
                    AuthorId = a.AuthorId,
                    AbuseAdvId = a.AbuseAdvId,
                    Priority = a.Priority,
                    AbuseText = a.AbuseText,
                    RemovedDate = a.RemovedDate
                }),
                Total = total,
                Offset = request.Offset,
                Limit = request.Limit
            };
        }
    }
}
