using Application.Abstractions.Data;
using Data.Generator;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[Route("test-data")]
public class TestDataController : ApiController
{
    private readonly IApplicationDbContext _appDbContext;

    public TestDataController(ISender sender, IApplicationDbContext appDbContext) : base(sender)
    {
        _appDbContext = appDbContext;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateAsync(CancellationToken cancellationToken)
    {
        var providers = ContractDataGenerator.GenerateCompanies([], 5);
        var clients = ContractDataGenerator.GenerateCompanies(providers.Select(x => x.Code).ToHashSet(), 5);

        await _appDbContext.Companies.AddRangeAsync(providers, cancellationToken);
        await _appDbContext.Companies.AddRangeAsync(clients, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        var contracts = ContractDataGenerator.GenerateContracts(providers, clients);

        await _appDbContext.Contracts.AddRangeAsync(contracts, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        var agreements = ContractDataGenerator.GenerateAgreements(contracts);

        await _appDbContext.Agreements.AddRangeAsync(agreements, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        var workHours = ContractDataGenerator.GenerateWorkHours(agreements);

        await _appDbContext.WorkHours.AddRangeAsync(workHours, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}
