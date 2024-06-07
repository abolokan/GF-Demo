namespace Application.Requests.Contracts.Queries.GetContractsByYear;

public sealed record ContractsByYearResponse(Guid CompanyId, string CompanyName, Guid ContractId, string ContractName, List<HourlyPrice> HourlyPrices);
public sealed record HourlyPrice(DateOnly StartDateAgreement, decimal PricePerHour, int Month);
