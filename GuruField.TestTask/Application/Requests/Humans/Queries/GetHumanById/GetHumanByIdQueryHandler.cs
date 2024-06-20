using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Requests.Humans.Queries.GetHumanById;

internal sealed class GetHumanByIdQueryHandler : IQueryHandler<GetHumanByIdQuery, GetHumanByIdQueryResponse>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetHumanByIdQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<GetHumanByIdQueryResponse>> Handle(GetHumanByIdQuery query, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Humans
            .AsNoTracking()
            .Where(x => x.Id == query.PersonId)
            .Select(x => new GetHumanByIdQueryResponse(x.Id, x.Name, x.MostFavoriteAnimal.Name, x.LeastFavoriteAnimal.Name))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
