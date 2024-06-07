using Application.Abstractions.Messaging;

namespace Application.Requests.Members.Queries.GetMemberById;

public sealed record class GetContractsByYearQuery(int Year) : IQuery<List<ContractsByYearResponse>>;