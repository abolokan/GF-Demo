using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Requests.Humans.Commands.AssignLeastFavoriteAnimal
{
    internal class AssignLeastFavoriteAnimalCommandHandler : ICommandHandler<AssignLeastFavoriteAnimalCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public AssignLeastFavoriteAnimalCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Result> Handle(AssignLeastFavoriteAnimalCommand command, CancellationToken cancellationToken)
        {
            var human = await _applicationDbContext.Humans.FirstOrDefaultAsync(x => x.Id == command.PersonId);
            if (human == null)
            {
                return Result.Failure(new Error("humman.error", "human not founbd"));
            }

            human.SetLeastFavoriteAnimalId(command.AnimalId);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
