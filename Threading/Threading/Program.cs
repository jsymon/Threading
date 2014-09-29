using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadSimple();
            ThreadParameter();
            ThreadCommon();
            AsyncWait();
            AsyncAwait();
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        private static void ThreadSimple()
        {
            var ts = new ThreadStart(delegate() { Console.WriteLine("ThreadSimple"); });
            var thread = new Thread(ts);
            thread.Start();
            thread.Join();
        }

        private static void ThreadParameter()
        {
            var pts = new ParameterizedThreadStart((object obj) => { Console.WriteLine(string.Format("ThreadParameter (parameter is of type '{0}')", obj == null ? "null" : obj.GetType().ToString())); });
            var thread = new Thread(pts);
            thread.Start("hello world");
            thread.Join();
        }

        /// <summary>
        /// This is probably the most common way to call thread - defining an inline delegate
        /// </summary>
        private static void ThreadCommon()
        {
            var thread = new Thread(() => { Console.WriteLine("ThreadCommon"); });
            thread.Start();
            thread.Join();
        }

        private static void AsyncWait()
        {
            var task = AsyncMethod(1);
            Console.WriteLine("AsyncSimple caller");
            task.Wait();
            Console.WriteLine(task.Result);
        }

        /// <summary>
        /// This is not very nice - fully asynchronous yet returns void so is uncontrollable!
        /// </summary>
        private static async void AsyncAwait()
        {
            var task = await AsyncMethod(2);
            Console.WriteLine("AsyncSimple caller 2");
            Console.WriteLine(task);
        }

        private static async Task<string> AsyncMethod(int instance)
        {
            Console.WriteLine("AsyncSimple start " + instance);
            await Task.Delay(1000);
            //horrible syntax!
            await Task.Run(new Func<Task>(() => { return Task.Delay(1000); }));
            return "AsyncSimple end " + instance;
        }
    }
}
