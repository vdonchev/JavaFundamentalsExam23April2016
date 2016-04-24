namespace _03.CriticalBreakpoint
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public static class CriticalBreakpoint
    {
        private static List<Tuple<BigInteger, BigInteger, BigInteger, BigInteger>> lines;

        public static void Main()
        {
            lines = new List<Tuple<BigInteger, BigInteger, BigInteger, BigInteger>>();
            var criticalBreakpoint = false;
            BigInteger? criticalRatio = null;

            while (true)
            {
                var input = Console.ReadLine();
                if (input.Equals("Break it."))
                {
                    break;
                }

                var lineCoordinates = input
                    .Split(' ')
                    .Select(BigInteger.Parse)
                    .ToArray();

                var x1 = lineCoordinates[0];
                var y1 = lineCoordinates[1];
                var x2 = lineCoordinates[2];
                var y2 = lineCoordinates[3];

                var currentCriticalRatio = (x2 + y2) - (x1 + y1);
                if (currentCriticalRatio < 0)
                {
                    currentCriticalRatio *= -1;
                }

                lines.Add(new Tuple<BigInteger, BigInteger, BigInteger, BigInteger>(
                    x1, y1, x2, y2));

                if (criticalRatio != null &&
                    currentCriticalRatio != criticalRatio.Value &&
                    currentCriticalRatio != 0)
                {
                    criticalBreakpoint = false;
                    break;
                }

                if (criticalRatio == null &&
                    currentCriticalRatio != 0)
                {
                    criticalBreakpoint = true;
                    criticalRatio = currentCriticalRatio;
                }
            }

            if (criticalBreakpoint)
            {
                foreach (var line in lines)
                {
                    Console.WriteLine($"Line: [{line.Item1}, {line.Item2}, {line.Item3}, {line.Item4}]");
                }

                var critical =
                    Power(criticalRatio.Value, lines.Count) % BigInteger.Parse(lines.Count.ToString());
                Console.WriteLine($"Critical Breakpoint: {critical}");
            }
            else
            {
                Console.WriteLine("Critical breakpoint does not exist.");
            }
        }

        private static BigInteger Power(BigInteger num, BigInteger pow)
        {
            var res = num;
            for (int i = 1; i < pow; i++)
            {
                res *= num;
            }

            return res;
        }
    }
}