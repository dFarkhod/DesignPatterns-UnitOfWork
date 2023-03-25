namespace UnitOfWorkDemo.Services
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }

    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}