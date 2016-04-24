using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.CriticalBreakpointV2
{
    using System.Numerics;

    class Program
    {
        static void Main(string[] args)
        {
            BigInteger UltimateCriticalRatio = BigInteger.Parse("0");
            var fail = false;
            var lines = new List<string>();

            var line = string.Empty;
            while ((line = Console.ReadLine()) != "Break it.")
            {
                var items = line.Split(' ');
                var numbers = new BigInteger[items.Length];
                for (int i = 0; i < items.Length; i++)
                {
                    numbers[i] = BigInteger.Parse(items[i]);
                }

                var x1 = numbers[0];
                var y1 = numbers[1];
                var x2 = numbers[2];
                var y2 = numbers[3];

                lines.Add("[" + x1 + ", " + y1 + ", " + x2 + ", " + y2 + "]");

                var currentCriticalRation = BigInteger.Subtract(
                    BigInteger.Add(x2, y2), 
                    BigInteger.Add(x1, y1));

                currentCriticalRation = BigInteger.Abs(currentCriticalRation);

                //assign the current critical ratio to the ultimate critical ratio - if the condition is met
                if (UltimateCriticalRatio.CompareTo(BigInteger.Zero) == 0)
                {
                    UltimateCriticalRatio = currentCriticalRation;
                }

                //check if there's a critical breakpoint
                if (currentCriticalRation.CompareTo(BigInteger.Zero) != 0)
                {
                    if (currentCriticalRation.CompareTo(UltimateCriticalRatio) != 0)
                    {
                        Console.WriteLine("Critical breakpoint does not exist.");
                        fail = true;
                        break;
                    }
                }
            }

            if (!fail)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    Console.WriteLine("Line: " + lines[i]);
                }

                Console.WriteLine("Critical Breakpoint: " + BigInteger.Remainder(
                    BigInteger.Pow(UltimateCriticalRatio, lines.Count),
                    BigInteger.Parse(lines.Count.ToString())));
            }
        }
    }
}
