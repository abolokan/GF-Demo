using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Requests.Humans.Queries.GetAllHumans;

internal sealed class GetAllHumansQueryHandler : IQueryHandler<GetAllHumansQuery, List<GetAllHumansQueryResponse>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetAllHumansQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<List<GetAllHumansQueryResponse>>> Handle(GetAllHumansQuery query, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Humans
            .AsNoTracking()
            .Select(x => new GetAllHumansQueryResponse(x.Id, x.Name))
            .ToListAsync(cancellationToken);
    }
}
