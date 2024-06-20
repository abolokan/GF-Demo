using Application.Abstractions.Messaging;

namespace Application.Requests.Animals.Queries.GetNotLinkedAnimals;

public sealed record GetNotLinkedAnimalsQuery() : IQuery<List<GetNotLinkedAnimalsQueryResponse>>;