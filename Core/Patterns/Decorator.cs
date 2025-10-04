namespace MarketplaceDemo.Core.Patterns
{
    public interface IService { void DoWork(); }

    public class RealService : IService
    {
        public void DoWork() => Console.WriteLine("Main service work executed.");
    }

    public class LoggingDecorator : IService
    {
        private readonly IService _inner;
        public LoggingDecorator(IService inner) => _inner = inner;

        public void DoWork()
        {
            Console.WriteLine("[Decorator] Start logging...");
            _inner.DoWork();
            Console.WriteLine("[Decorator] End logging...");
        }
    }
}
