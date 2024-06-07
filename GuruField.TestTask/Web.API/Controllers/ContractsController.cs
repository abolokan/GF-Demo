using Application.Requests.Members.Queries.GetMemberById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[Route("contracts")]
public class ContractsController : ApiController
{
    public ContractsController(ISender sender) : base(sender)
    {
    }

    [HttpGet("{year}")]
    public async Task<IActionResult> GetAsync(int year, CancellationToken cancellationToken)
    {
        var query = new GetContractsByYearQuery(year);
        var response = await Sender.Send(query, cancellationToken);
        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }
}
