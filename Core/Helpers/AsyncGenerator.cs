namespace MarketplaceDemo.Core.Helpers
{
    public class AsyncGenerator
    {
        public async IAsyncEnumerable<int> GenerateNumbers()
        {
            for (int i = 1; i <= 5; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }
    }
}
