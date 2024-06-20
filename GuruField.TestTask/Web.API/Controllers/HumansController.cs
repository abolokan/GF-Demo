using Application.Requests.Humans.Commands.AssignLeastFavoriteAnimal;
using Application.Requests.Humans.Commands.AssignMostFavoriteAnimal;
using Application.Requests.Humans.Queries.GetAllHumans;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[Route("humans")]
public class HumansController : ApiController
{
    public HumansController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllHumansQuery();
        var response = await Sender.Send(query, cancellationToken);
        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [HttpPatch("{personId}/most-favorite/{animalId}")]
    public async Task<IActionResult> AssignMostFavoriteAnimalAsync(Guid personId, Guid animalId, CancellationToken cancellationToken)
    {
        var query = new AssignMostFavoriteAnimalCommand(personId, animalId);
        var response = await Sender.Send(query, cancellationToken);
        return response.IsSuccess ? Ok(response) : HandleFailure(response);
    }

    [HttpPatch("{personId}/least-favorite/{animalId}")]
    public async Task<IActionResult> AssignLeastFavoriteAnimalAsync(Guid personId, Guid animalId, CancellationToken cancellationToken)
    {
        var query = new AssignLeastFavoriteAnimalCommand(personId, animalId);
        var response = await Sender.Send(query, cancellationToken);
        return response.IsSuccess ? Ok(response) : HandleFailure(response);
    }
}
