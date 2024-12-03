using System.Numerics;

namespace AsyncSprint
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            await Testing();
        }

        static async Task Testing()
        {
            Console.WriteLine("1");
            var testTask = Task.Run(async () => { await Task.Delay(5000); Console.WriteLine("Task finished!"); });
            await Task.WhenAll([testTask]);
        }

        static async Task readStory(List<string> story)
        {
            var task = Task.Run(async () =>
            {
                story.ForEach(x => { Thread.Sleep(1000); Console.WriteLine(x); });
            });



            await Task.WhenAll(task);
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




            var printHello = Task<string>.Run(async () =>
            {
                await Task.Delay(time1, token);

                return "Hello...";
                //Console.WriteLine("Hello...");


            }, token);


            var printWorld = Task<string>.Run(async () =>
            {
                await Task.Delay(time2, token);

                return "...World";
                //Console.WriteLine("...World");

            }, token);



            //var combinedResult = await Task.WhenAll([printHello, printWorld]);

            try
            {

                Console.WriteLine(printHello.Result + printWorld.Result);
                //await combinedResult;

            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine(e.Message);
                source.Cancel();
                source.Dispose();
            }


        }

        static async Task performCalculations(List<BigInteger> data)
        {


            //var tasksScheduled = data.Select(x => new Task(() => {
            //         Console.WriteLine(Exercises.CalculateFactorial(x));
            //         })).ToList();
            var tasksScheduled = data.Select(x => new Task<BigInteger>(() =>
            {
                return (Exercises.CalculateFactorial(x));
            })).ToList();


            foreach (Task task in tasksScheduled)
            {
                task.Start();
                Console.WriteLine("Task started.");
                task.ContinueWith(t => { Console.WriteLine("Task complete"); });
            }

            await Task.WhenAll(tasksScheduled).ContinueWith(t =>
            {
                List<BigInteger> result = t.Result.ToList();
                foreach (BigInteger x in result)
                {
                    Console.WriteLine($"Today's big number: {x} \n\n\n\n\n\n\n");
                }
            });
        }







    }
}
