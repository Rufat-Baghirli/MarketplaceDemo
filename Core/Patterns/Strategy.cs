namespace MarketplaceDemo.Core.Patterns
{
    public interface IStrategy
    {
        void Execute();
    }

    public class ConcreteStrategyA : IStrategy
    {
        public void Execute() => Console.WriteLine("Executing Strategy A");
    }

    public class StrategyContext
    {
        private IStrategy _strategy;
        public StrategyContext(IStrategy strategy) => _strategy = strategy;

        public void ExecuteStrategy() => _strategy.Execute();

        public List<int> FindEvenNumbers(List<int> numbers)
        {
            return numbers.Where(n => n % 2 == 0)
                          .OrderByDescending(n => n)
                          .Take(3)
                          .ToList();
        }
    }
}
