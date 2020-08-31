using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RunningProcess
{
    class Program
    {
        static void DoSomeWork(CancellationToken ct)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Running...");
                    Thread.Sleep(1000);

                    if (ct.IsCancellationRequested)
                    {
                        Console.WriteLine("Task cancelled");
                        ct.ThrowIfCancellationRequested();
                    }
                }
            }
            catch (OperationCanceledException)
            {

            }
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("STARTING");

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            Console.WriteLine("Press any key to begin tasks...");
            Console.ReadKey(true);
            Console.WriteLine("To terminate the example, press 'c' to cancel and exit...");
            Console.WriteLine();

            Task t = Task.Run(() => DoSomeWork( token), token);

            char ch = Console.ReadKey().KeyChar;
            if (ch == 'c' || ch == 'C')
            {
                tokenSource.Cancel();
                Console.WriteLine("\nTask cancellation requested.");
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
