using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Requests.Humans.Commands.AssignMostFavoriteAnimal
{
    internal class AssignMostFavoriteAnimalCommandHandler : ICommandHandler<AssignMostFavoriteAnimalCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public AssignMostFavoriteAnimalCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Result> Handle(AssignMostFavoriteAnimalCommand command, CancellationToken cancellationToken)
        {
            var human = await _applicationDbContext.Humans.FirstOrDefaultAsync(x => x.Id == command.PersonId);
            if (human == null)
            {
                return Result.Failure(new Error("humman.error", "human not founbd"));
            }

            human.SetMostFavoriteAnimalId(command.AnimalId);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
