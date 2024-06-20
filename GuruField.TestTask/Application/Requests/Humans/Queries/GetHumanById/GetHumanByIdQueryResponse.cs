namespace Application.Requests.Humans.Queries.GetHumanById;

public sealed record GetHumanByIdQueryResponse(Guid Id, string Name, string MostFavoriteAnimal, string LeastFavoriteAnimal);
