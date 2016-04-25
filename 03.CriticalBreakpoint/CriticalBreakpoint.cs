namespace _03.CriticalBreakpoint
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public static class CriticalBreakpoint
    {
        private static List<Line> lines;

        public static void Main()
        {
            lines = new List<Line>();
            while (true)
            {
                var input = Console.ReadLine();
                if (input.Equals("Break it."))
                {
                    break;
                }

                var lineCoordinates = input
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(BigInteger.Parse)
                    .ToArray();

                lines.Add(
                    new Line(
                        lineCoordinates[0],
                        lineCoordinates[1],
                        lineCoordinates[2],
                        lineCoordinates[3]));
            }

            var criticalBreakpoint = false;
            BigInteger? criticicalRatio = null;

            foreach (var line in lines)
            {
                if (criticicalRatio != null &&
                    line.CriticalRatio != criticicalRatio.Value &&
                    line.CriticalRatio != 0)
                {
                    criticalBreakpoint = false;
                    break;
                }

                if (criticicalRatio == null &&
                    line.CriticalRatio != 0)
                {
                    criticalBreakpoint = true;
                    criticicalRatio = line.CriticalRatio;
                }
            }

            if (criticalBreakpoint)
            {
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }

                var critical = BigInteger.Pow(criticicalRatio.Value, lines.Count) % lines.Count;

                Console.WriteLine($"Critical Breakpoint: {critical}");
            }
            else
            {
                Console.WriteLine("Critical breakpoint does not exist.");
            }
        }

        private class Line
        {
            private readonly BigInteger x1;
            private readonly BigInteger y1;
            private readonly BigInteger x2;
            private readonly BigInteger y2;

            public Line(BigInteger x1, BigInteger y1, BigInteger x2, BigInteger y2)
            {
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
            }

            public BigInteger CriticalRatio
            {
                get
                {
                    return BigInteger.Abs((this.x2 + this.y2) - (this.x1 + this.y1));
                }
            }

            public override string ToString()
            {
                return $"Line: [{this.x1}, {this.y1}, {this.x2}, {this.y2}]";
            }
        }
    }
}