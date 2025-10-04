namespace MarketplaceDemo.Core.Patterns
{
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> _instance = new(() => new Singleton());
        public static Singleton Instance => _instance.Value;

        private Singleton() { }

        public void Log(string message)
        {
            Console.WriteLine($"[Singleton] {message}");
        }
    }
}
