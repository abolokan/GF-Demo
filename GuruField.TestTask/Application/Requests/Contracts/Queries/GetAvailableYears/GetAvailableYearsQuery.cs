using Application.Abstractions.Messaging;

namespace Application.Requests.Contracts.Queries.GetContractsByYear;

public sealed record class GetAvailableYearsQuery() : IQuery<List<int>>;