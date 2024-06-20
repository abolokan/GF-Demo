using Application.Abstractions.Messaging;

namespace Application.Requests.Humans.Queries.GetAllHumans;

public sealed record GetAllHumansQuery() : IQuery<List<GetAllHumansQueryResponse>>;