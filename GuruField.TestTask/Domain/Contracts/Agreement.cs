namespace Domain.Contracts;

public class Agreement : Entity
{
    private readonly List<WorkHour> _workData = new();

    internal Agreement(Guid id, Guid contractId, Money hourlyPrice, DateOnly startDate)
        : base(id)
    {
        ContractId = contractId;
        HourlyPrice = hourlyPrice;
        StartDate = startDate;
    }

    private Agreement() { }

    public Money HourlyPrice { get; private set; }

    public DateOnly StartDate { get; private set; }

    public Guid ContractId { get; private set; }
    public Contract Contract { get; set; } = default!;

    public IReadOnlyList<WorkHour> WorkData => _workData;

    public static Agreement Create(Guid contractId, decimal hourlyPrice, string currency, DateOnly startDate)
    {
        var agreement = new Agreement(Guid.NewGuid(), contractId, new Money(currency, hourlyPrice), startDate);

        agreement.Raise(new AgreementCreatedDomainEvent(agreement.Id));

        return agreement;
    }

    public void AddWorkHours(List<WorkHour> workHours)
    {
        _workData.AddRange(workHours);
    }
}
