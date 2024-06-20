using Application.Abstractions.Messaging;

namespace Application.Requests.Animals.Queries.GetAllAnimals;

public sealed record GetAllAnimalsQuery() : IQuery<List<GetAllAnimalsQueryResponse>>;