namespace MarketplaceDemo.Core.Helpers;

public static class Extensions
{
    public static int Square(this int value)
    {
        var squared = value * value;
        Console.WriteLine($"[Extension] {value}^2 = {squared}");
        return squared;
    }
}
