using Application.Requests.Animals.Commands.AssignFavoritePray;
using Application.Requests.Animals.Queries.GetAllAnimals;
using Application.Requests.Animals.Queries.GetNotLinkedAnimals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[Route("animals")]
public class AnimalsController : ApiController
{
    public AnimalsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllAnimalsQuery();
        var response = await Sender.Send(query, cancellationToken);
        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [HttpGet("not-linked")]
    public async Task<IActionResult> GetNotLinkedAnimalsQueryAsync(CancellationToken cancellationToken)
    {
        var query = new GetNotLinkedAnimalsQuery();
        var response = await Sender.Send(query, cancellationToken);
        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [HttpPost("{predatorId}/hunt/{preyId}")]
    public async Task<IActionResult> AssignFavoritePreyAsync(Guid predatorId, Guid preyId, CancellationToken cancellationToken)
    {
        var query = new AssignFavoritePreyCommand(predatorId, preyId);
        var response = await Sender.Send(query, cancellationToken);
        return response.IsSuccess ? Ok(response) : HandleFailure(response);
    }
}
