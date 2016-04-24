namespace _02.Crossfire
{
    using System;
    using System.Linq;
    using System.Text;

    public static class Crossfire
    {
        private static int[][] matrix;

        public static void Main()
        {
            var matrixDimensions = ReadInput(Console.ReadLine());
            var rows = matrixDimensions[0];
            var cols = matrixDimensions[1];

            matrix = new int[rows][];
            FillMatrix(rows, cols);

            var command = Console.ReadLine();
            while (command != "Nuke it from orbit")
            {
                var commandTokens = ReadInput(command);
                var row = commandTokens[0];
                var col = commandTokens[1];
                var radius = commandTokens[2];

                var top = Math.Max(0, row - radius);
                var bottom = Math.Min(rows - 1, row + radius);
                var left = Math.Max(0, col - radius);
                var right = Math.Min(cols - 1, col + radius);

                if (col >= 0 && col < cols)
                {
                    for (int r = top; r <= bottom; r++)
                    {
                        matrix[r][col] = 0;
                    }
                }

                if (row >= 0 && row < rows)
                {
                    for (int c = left; c <= right; c++)
                    {
                        matrix[row][c] = 0;
                    }
                }

                for (int r = 0; r < rows; r++)
                {
                    var unusedIndex = -1;
                    for (int c = 0; c < cols; c++)
                    {
                        if (matrix[r][c] == 0 &&
                            unusedIndex < 0)
                        {
                            unusedIndex = c;
                        }
                        else if (matrix[r][c] > 0 &&
                          unusedIndex >= 0)
                        {
                            Swap(ref matrix[r][c], ref matrix[r][unusedIndex]);
                            unusedIndex++;
                        }
                    }
                }

                for (int r = 0; r < rows - 1; r++)
                {
                    if (matrix[r].Sum() == 0)
                    {
                        var temp = matrix[r];
                        matrix[r] = matrix[r + 1];
                        matrix[r + 1] = temp;
                    }
                }

                command = Console.ReadLine();
            }

            PrintMatrix();
        }

        private static void Swap(ref int item1, ref int item2)
        {
            var temp = item1;
            item1 = item2;
            item2 = temp;
        }

        private static void FillMatrix(int rows, int cols)
        {
            var index = 1;
            for (int row = 0; row < rows; row++)
            {
                matrix[row] = new int[cols];
                for (int col = 0; col < cols; col++)
                {
                    matrix[row][col] = index++;
                }
            }
        }

        private static void PrintMatrix()
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                var builder = new StringBuilder();
                for (int col = 0; col < matrix[0].Length; col++)
                {
                    if (matrix[row][col] != 0)
                    {
                        builder.Append($"{matrix[row][col]} ");
                    }
                }
                if (builder.Length > 0)
                {
                    Console.WriteLine(builder);
                }
            }
        }

        private static int[] ReadInput(string input)
        {
            var res = input
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            return res;
        }
    }
}
