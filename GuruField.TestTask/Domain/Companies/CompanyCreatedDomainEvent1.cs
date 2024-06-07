using Shared;

namespace Domain.Companies;

public sealed record CompanyCreatedDomainEvent(Guid CompanyId) : IDomainEvent;