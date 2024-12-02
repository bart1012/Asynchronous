using System.Numerics;

namespace AsyncSprint
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            List<BigInteger> dataList = "85671 34262 92143 50984 24515 68356 77247 12348 56789 98760".Split(' ')
                .Select(x => BigInteger.Parse(x)).ToList();

            List<string> story = "Mary had a little lamb, its fleece was white as snow.".Split(' ').ToList();

            readStory(story);
            await performCalculations(dataList);
            //await printConsole();

            //await readStory(story);
            //Thread.Sleep(5000);
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
