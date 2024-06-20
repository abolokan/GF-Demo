using Application.Abstractions.Messaging;

namespace Application.Requests.Contracts.Queries.GetContractsByYear;

public sealed record GetAvailableYearsQuery() : IQuery<List<int>>;