using Shared;

namespace Domain.Humans;

public sealed record PersonCreatedDomainEvent(Guid personId) : IDomainEvent;