namespace MarketplaceDemo.Core.Helpers
{
    public class DeadlockDemo
    {
        private readonly object _lockA = new();
        private readonly object _lockB = new();

        public void RunUnsafe()
        {
            var t1 = new Thread(MethodA);
            var t2 = new Thread(MethodB);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
        }

        private void MethodA()
        {
            lock (_lockA)
            {
                Console.WriteLine("Thread 1 locked A");
                Thread.Sleep(100);
                lock (_lockB)
                {
                    Console.WriteLine("Thread 1 locked B");
                }
            }
        }

        private void MethodB()
        {
            lock (_lockB)
            {
                Console.WriteLine("Thread 2 locked B");
                Thread.Sleep(100);
                lock (_lockA)
                {
                    Console.WriteLine("Thread 2 locked A");
                }
            }
        }

        public void RunSafe()
        {
            var t1 = new Thread(SafeMethodA);
            var t2 = new Thread(SafeMethodB);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
        }

        private void SafeMethodA()
        {
            lock (_lockA)
            {
                Console.WriteLine("Thread 1 safely locked A");
                Thread.Sleep(100);
                lock (_lockB)
                {
                    Console.WriteLine("Thread 1 safely locked B");
                }
            }
        }

        private void SafeMethodB()
        {
            lock (_lockA)
            {
                Console.WriteLine("Thread 2 safely locked A");
                Thread.Sleep(100);
                lock (_lockB)
                {
                    Console.WriteLine("Thread 2 safely locked B");
                }
            }
        }
    }
}
