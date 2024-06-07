using Domain.Contracts;

namespace Domain.Companies;

public class Company : Entity
{
    private readonly List<Contract> _providerContracts = new();
    private readonly List<Contract> _clientContracts = new();

    public Company(Guid id, string code, string name)
        : base(id)
    {
        Code = code;
        Name = name;
    }

    public string Code { get; private set; }
    public string Name { get; private set; }

    public IReadOnlyList<Contract> ProviderContracts => _providerContracts;
    public IReadOnlyList<Contract> ClientContracts => _clientContracts;

    public static Company Create(string code, string name)
    {
        var company = new Company(Guid.NewGuid(), code, name);

        company.Raise(new CompanyCreatedDomainEvent(company.Id));

        return company;
    }

    public void AddProviderContracts(List<Contract> contracts)
    {
        _providerContracts.AddRange(contracts);
    }

    public void AddClientContracts(List<Contract> contracts)
    {
        _clientContracts.AddRange(contracts);
    }
}
