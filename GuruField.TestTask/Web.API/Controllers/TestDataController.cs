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

    [HttpPost("generate/{fromYear}/{toYear}")]
    public async Task<IActionResult> GenerateContractsAsync(int fromYear, int toYear, CancellationToken cancellationToken)
    {
        var generator = new ContractDataGenerator();
        var providers = generator.GenerateCompanies([], 5);
        var clients = generator.GenerateCompanies(providers.Select(x => x.Code).ToHashSet(), 5);

        await _appDbContext.Companies.AddRangeAsync(providers, cancellationToken);
        await _appDbContext.Companies.AddRangeAsync(clients, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        var contracts = generator.GenerateContracts(fromYear, toYear, providers, clients);

        await _appDbContext.Contracts.AddRangeAsync(contracts, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        var agreements = generator.GenerateAgreements(contracts);

        await _appDbContext.Agreements.AddRangeAsync(agreements, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        var workHours = generator.GenerateWorkHours(agreements);

        await _appDbContext.WorkHours.AddRangeAsync(workHours, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Ok();
    }


    [HttpPost("generate/animals/{count}")]
    public async Task<IActionResult> GenerateAnimalsAsync(int count, CancellationToken cancellationToken)
    {
        var generator = new AnimalGenerator();
        var animals = generator.GenerateAnimals(count);

        await _appDbContext.Animals.AddRangeAsync(animals, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Ok(animals);
    }

    [HttpPost("generate/humans/{count}")]
    public async Task<IActionResult> GenerateHumansAsync(int count, CancellationToken cancellationToken)
    {
        var generator = new HumanGenerator();
        var humans = generator.GenerateHumans(count);

        await _appDbContext.Humans.AddRangeAsync(humans, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Ok(humans);
    }
}
