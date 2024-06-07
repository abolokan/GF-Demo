using Shared;

namespace Domain.Contracts;

public sealed record AgreementCreatedDomainEvent(Guid agreementId) : IDomainEvent;