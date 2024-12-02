using System.Numerics;

namespace AsyncSprint
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            List<BigInteger> dataList = "85671 34262 92143 50984 24515 68356 77247 12348 56789 98760".Split(' ')
                .Select(x => BigInteger.Parse(x)).ToList();

            await performCalculations(dataList);

        }



        static async Task printConsole()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            var token = source.Token;
            int timeout = 5000;
            source.CancelAfter(timeout);

            Random random = new Random();
            int time1 = random.Next(10000);
            Console.WriteLine(time1);
            int time2 = random.Next(10000);
            Console.WriteLine(time2);




            var printHello = Task.Run(async () =>
            {
                await Task.Delay(time1, token);

                Console.WriteLine("Hello...");


            }, token);


            var printWorld = Task.Run(async () =>
            {
                await Task.Delay(time2, token);

                Console.WriteLine("...World");

            }, token);



            var combinedResult = Task.WhenAll([printHello, printWorld]);

            try
            {
                await combinedResult;

            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
                source.Cancel();
                source.Dispose();
            }


        }

        static async Task performCalculations(List<BigInteger> data)
        {


            foreach (var element in data)
            {
                var output = Exercises.CalculateFactorial(element);
                Console.WriteLine(output);
            }


        }


    }
}
