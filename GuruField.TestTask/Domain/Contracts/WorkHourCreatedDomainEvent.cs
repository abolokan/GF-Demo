using Shared;

namespace Domain.Contracts;

public sealed record class WorkHourCreatedDomainEvent(Guid whId) : IDomainEvent;