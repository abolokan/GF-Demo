using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Requests.Animals.Queries.GetNotLinkedAnimals;

internal sealed class GetNotLinkedAnimalsQueryHandler : IQueryHandler<GetNotLinkedAnimalsQuery, List<GetNotLinkedAnimalsQueryResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetNotLinkedAnimalsQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public async Task<Result<List<GetNotLinkedAnimalsQueryResponse>>> Handle(GetNotLinkedAnimalsQuery query, CancellationToken cancellationToken)
    {
        return await (
            from animal in _context.Animals

            join personAndMostFavorit in _context.Humans on animal.Id equals personAndMostFavorit.MostFavoriteAnimalId into mostFavorites
            from mostFavoriteAnimal in mostFavorites.DefaultIfEmpty()

            join personAndLestFavorit in _context.Humans on animal.Id equals personAndLestFavorit.LeastFavoriteAnimalId into lestFavorits
            from lestFavoritAnimal in lestFavorits.DefaultIfEmpty()

            join predator in _context.Predators on animal.Id equals predator.PredatorId into predators
            from predorLink in predators.DefaultIfEmpty()

            join favoritePrey in _context.Predators on animal.Id equals favoritePrey.FavoritePreyId into favoritPreys
            from favoritPreyLink in favoritPreys.DefaultIfEmpty()

            where mostFavoriteAnimal == null && lestFavoritAnimal == null && predorLink == null && favoritPreyLink == null

            select new GetNotLinkedAnimalsQueryResponse(animal.Id, animal.Name))

            .ToListAsync(cancellationToken);
    }
}
