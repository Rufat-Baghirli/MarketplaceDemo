namespace MarketplaceDemo.Core.Patterns
{
    public interface ILogger
    {
        void Log(string message);
    }

    public class FileLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine($"[FileLog] {message}");
    }

    public abstract class LoggerFactory
    {
        public abstract ILogger CreateLogger();
    }

    public class FileLoggerFactory : LoggerFactory
    {
        public override ILogger CreateLogger() => new FileLogger();
    }
}
