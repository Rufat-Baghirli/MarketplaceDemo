namespace MarketplaceDemo.Core.Patterns
{
    public interface ITarget
    {
        void Request();
    }

    public class Adaptee
    {
        public void SpecificRequest() => Console.WriteLine("Called SpecificRequest()");
    }

    public class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;
        public Adapter(Adaptee adaptee) => _adaptee = adaptee;

        public void Request() => _adaptee.SpecificRequest();
    }
}
