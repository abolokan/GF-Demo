using Application.Abstractions.Messaging;

namespace Application.Requests.Contracts.Queries.GetContractsByYear;

public sealed record GetContractsByYearQuery(int Year) : IQuery<List<ContractsByYearResponse>>;