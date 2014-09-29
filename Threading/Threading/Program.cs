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
            AsyncSimple();
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

        private static void AsyncSimple()
        {
            var task = AsyncMethod();
            Console.WriteLine("AsyncSimple caller");
            task.Wait();
        }

        private static async Task<string> AsyncMethod()
        {
            Console.WriteLine("AsyncSimple start");
            await Task.Delay(2000);
            Console.WriteLine("AsyncSimple end");
            return "AsyncMethod Return";
        }
    }
}
