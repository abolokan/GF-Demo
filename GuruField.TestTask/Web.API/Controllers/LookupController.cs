using Application.Requests.Contracts.Queries.GetContractsByYear;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[Route("lookup")]
public class LookupController : ApiController
{
    public LookupController(ISender sender) : base(sender)
    {
    }

    [HttpGet("available-years")]
    public async Task<IActionResult> GetYearsAsync(CancellationToken cancellationToken)
    {
        var query = new GetAvailableYearsQuery();
        var response = await Sender.Send(query, cancellationToken);
        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }
}
