using Shared;

namespace Domain.Companies;

public sealed record AgreementCreatedDomainEvent(Guid Id) : IDomainEvent;