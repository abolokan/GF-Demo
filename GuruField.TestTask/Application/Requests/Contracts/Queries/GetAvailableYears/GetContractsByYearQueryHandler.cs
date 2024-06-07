using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Requests.Contracts.Queries.GetContractsByYear;

internal sealed class GetAvailableYearsQueryHandler : IQueryHandler<GetAvailableYearsQuery, List<int>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetAvailableYearsQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<List<int>>> Handle(GetAvailableYearsQuery query, CancellationToken cancellationToken)
    {
        var initial = await _applicationDbContext.WorkHours
            .Select(x => x.Year)
            .Distinct()
            .ToListAsync(cancellationToken);

        return initial.OrderBy(x => x).ToList();
    }
}
