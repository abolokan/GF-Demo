using Domain.Companies;

namespace Domain.Contracts;

public class Contract : Entity
{
    private readonly List<Agreement> _agreements = new();

    internal Contract(Guid id, Guid clientId, Guid providerId, string name, DateOnly activeFrom, DateOnly? activeTo = null)
        : base(id)
    {
        Id = id;
        ClientId = clientId;
        ProviderId = providerId;
        Name = name;
        ActiveFrom = activeFrom;
        ActiveTo = activeTo;
    }

    private Contract()
    {

    }
    public string Name { get; private set; }

    public DateOnly ActiveFrom { get; private set; }
    public DateOnly? ActiveTo { get; private set; }
    public ContractState State { get; private set; }

    public Guid ClientId { get; private set; }
    public Company Client { get; private set; } = default!;

    public Guid ProviderId { get; private set; }
    public Company Provider { get; private set; } = default!;

    public IReadOnlyList<Agreement> Agreements => _agreements;

    public static Contract Create(Guid clientId, Guid providerId, string name, DateOnly activeFrom, DateOnly? activeTo = null)
    {
        var contract = new Contract(Guid.NewGuid(), clientId, providerId, name, activeFrom, activeTo);

        contract.Raise(new ContractCreatedDomainEvent(contract.Id));

        return contract;
    }

    public void SetLastDate(DateOnly date)
    {
        ActiveTo = date;
    }

    public void AddAgreements(List<Agreement> agreements)
    {
        _agreements.AddRange(agreements);
    }
}
