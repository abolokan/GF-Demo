namespace Application.Requests.Members.Queries.GetMemberById;

public sealed record ContractsByYearResponse(Guid CompanyId, string CompanyName, Guid ContractId, string ContractName, List<HourlyPrice> HourlyPrices);
public sealed record HourlyPrice(decimal PricePerHour, int Month);
