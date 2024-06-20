using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Requests.Animals.Queries.GetAllAnimals;

internal sealed class GetAllAnimalsQueryHandler : IQueryHandler<GetAllAnimalsQuery, List<GetAllAnimalsQueryResponse>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetAllAnimalsQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<List<GetAllAnimalsQueryResponse>>> Handle(GetAllAnimalsQuery query, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Animals
            .AsNoTracking()
            .Select(x => new GetAllAnimalsQueryResponse(x.Id, x.Name))
            .ToListAsync(cancellationToken);
    }
}
