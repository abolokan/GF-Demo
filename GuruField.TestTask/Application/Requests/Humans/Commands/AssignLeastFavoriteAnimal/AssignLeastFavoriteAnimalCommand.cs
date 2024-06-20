using Application.Abstractions.Messaging;

namespace Application.Requests.Humans.Commands.AssignLeastFavoriteAnimal
{
    public sealed record AssignLeastFavoriteAnimalCommand(Guid PersonId, Guid AnimalId) : ICommand;
}
