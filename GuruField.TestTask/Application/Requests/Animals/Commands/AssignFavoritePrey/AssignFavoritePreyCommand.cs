using Application.Abstractions.Messaging;

namespace Application.Requests.Animals.Commands.AssignFavoritePray
{
    public sealed record AssignFavoritePreyCommand(Guid PredatorId, Guid PreyId) : ICommand;
}
