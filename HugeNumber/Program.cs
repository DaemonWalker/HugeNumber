using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HugeNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            IntMax.StepLength = 10000;
            var n1 = new IntMax(-19543);
            var n2 = new IntMax(782);
            Console.WriteLine(n1 - n2);
            foo();
        }
        static void foo()
        {
            var random = new Random();
            var sw = new Stopwatch();
            while (true)
            {
                var r = new IntMax();
                var res = 0l;
                sw.Restart();
                for (int k = 0; k < 1000000; k++)
                {
                    var num = k;//random.Next(1, 100000000);
                    r = r - num;
                }
                sw.Stop();
                var time1 = sw.ElapsedMilliseconds;
                sw.Restart();
                for (int k = 0; k < 1000000; k++)
                {
                    var num = k;//random.Next(1, 100000000);
                    if (k % 2 == 0)
                    {
                        res = res + num;
                    }
                    else
                    {
                        res = res - num;
                    }
                }
                sw.Stop();
                var time2 = sw.ElapsedMilliseconds;
                Console.WriteLine($"Result: {res == r} Long: {res} IntMax: {r} Time1: {time1 / 1000.0} Time2: {time2 / 1000.0}");
                Thread.Sleep(1);
            }
        }
    }
}
