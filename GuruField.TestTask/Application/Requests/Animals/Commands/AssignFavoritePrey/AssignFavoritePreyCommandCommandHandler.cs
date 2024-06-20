using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Requests.Animals.Commands.AssignFavoritePray;
using Domain.Animals;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Requests.Animals.Commands.AssignFavoritePrey
{
    internal class AssignFavoritePreyCommandCommandHandler : ICommandHandler<AssignFavoritePreyCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public AssignFavoritePreyCommandCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Result> Handle(AssignFavoritePreyCommand command, CancellationToken cancellationToken)
        {
            var link = await _applicationDbContext.Predators
                .Where(x => x.Id == command.PredatorId && x.FavoritePreyId == command.PreyId)
                .FirstOrDefaultAsync();

            if (link != null)
            {
                return Result.Failure(new Error("predator.error", "It has assigned already"));
            }

            var predator = Predator.Create(command.PredatorId, command.PreyId);

            await _applicationDbContext.Predators.AddAsync(predator);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
