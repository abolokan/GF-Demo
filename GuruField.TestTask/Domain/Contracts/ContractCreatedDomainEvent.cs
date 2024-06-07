using Shared;

namespace Domain.Contracts;

public sealed record ContractCreatedDomainEvent(Guid contractId) : IDomainEvent;