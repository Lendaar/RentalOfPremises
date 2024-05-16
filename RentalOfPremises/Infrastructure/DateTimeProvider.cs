using RentalOfPremises.Common;

namespace RentalOfPremises.Api.Infrastructure
{
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTimeOffset IDateTimeProvider.UtcNow => DateTimeOffset.UtcNow;
    }
}
