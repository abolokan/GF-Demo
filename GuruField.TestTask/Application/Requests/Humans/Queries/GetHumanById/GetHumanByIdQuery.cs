using Application.Abstractions.Messaging;

namespace Application.Requests.Humans.Queries.GetHumanById;

public sealed record GetHumanByIdQuery(Guid PersonId) : IQuery<GetHumanByIdQueryResponse>;