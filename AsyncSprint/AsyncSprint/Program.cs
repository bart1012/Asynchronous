namespace AsyncSprint
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            CancellationToken source = new CancellationToken();
            await printConsole(source);
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }

        

        static async Task printConsole(CancellationToken token)
        {
            var timer = System.Diagnostics.Stopwatch.StartNew();

            Random random = new Random();
            int time1 = random.Next(10000);
            int time2 = random.Next(10000);
            

           

            var printHello = Task.Run(async () =>
            {
                Thread.Sleep(time1);
                Console.WriteLine("Hello...");   
                

            });

            var printWorld = Task.Run(async () =>
            {
                Thread.Sleep(time2);
                Console.WriteLine("...World");
                token.Cancel();
                
            });

            var cancelAfterTime = Task.Run(async () =>
            {
            
                if (timer.ElapsedMilliseconds > 5000)
                {
                    source.Cancel();
                    
                }
            });
            while (!source.IsCancellationRequested)
            {
                try
                {
                    await Task.WhenAll([printHello, printWorld]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            
        }
    }
}
