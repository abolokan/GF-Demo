using Application.Abstractions.Messaging;

namespace Application.Requests.Humans.Commands.AssignMostFavoriteAnimal
{
    public sealed record AssignMostFavoriteAnimalCommand(Guid PersonId, Guid AnimalId) : ICommand;
}
