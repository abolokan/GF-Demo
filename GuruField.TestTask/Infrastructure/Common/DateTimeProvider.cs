using Shared;

namespace Infrastructure.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime DateTime => DateTime.UtcNow;
}
