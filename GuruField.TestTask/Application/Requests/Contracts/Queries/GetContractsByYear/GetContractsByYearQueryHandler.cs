using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Requests.Members.Queries.GetMemberById;

internal sealed class GetContractsByYearQueryHandler : IQueryHandler<GetContractsByYearQuery, List<ContractsByYearResponse>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetContractsByYearQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<List<ContractsByYearResponse>>> Handle(GetContractsByYearQuery query, CancellationToken cancellationToken)
    {
        var initialData = await (from company in _applicationDbContext.Companies
                           join contract in _applicationDbContext.Contracts on company.Id equals contract.ProviderId
                           join agreement in _applicationDbContext.Agreements on contract.Id equals agreement.ContractId
                           join wh in _applicationDbContext.WorkHours on agreement.Id equals wh.AgreementId
                           where wh.Year == query.Year
                           select new
                           {
                               CompanyId = company.Id,
                               CompanyName = company.Name,
                               ContractId = contract.Id,
                               ContractName = contract.Name,
                               HourlyPrice = agreement.HourlyPrice.Amount,
                               wh.Month
                           })
                           .ToListAsync(cancellationToken);

        var result = initialData.GroupBy(x => new { x.CompanyId, x.CompanyName, x.ContractId, x.ContractName })
                          .Select(grouped => new ContractsByYearResponse(
                              grouped.Key.CompanyId,
                              grouped.Key.CompanyName,
                              grouped.Key.ContractId,
                              grouped.Key.ContractName,
                              [.. grouped.Select(a => new HourlyPrice(a.HourlyPrice, a.Month)).OrderBy(x => x.Month)]
                          )).ToList();

        return result;
    }
}
