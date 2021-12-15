namespace AdventOfCode2021
{
    public static class MatrixExtensions
    {
        public static void ForEachAdjacentWithDiagonal(this int[][] matrix, int x, int y, Action<int, int, int> action)
        {
            for (int incY = -1; incY <= 1; incY++)
            {
                for (int incX = -1; incX <= 1; incX++)
                {
                    if ((incX != 0 || incY != 0) && InBounds(matrix, x + incX, y + incY))
                    {
                        action.Invoke(matrix[y + incY][x + incX], x + incX, y + incY);
                    }
                }
            }
        }

        public static void ForEachAdjacent(this int[][] matrix, int x, int y, Action<int, int, int> action)
        {
            var adjacents = new (int,int)[]{ (1, 0), (-1, 0), (0, 1), (0, -1) };
            for (int i = 0; i < adjacents.Length; i++)
            {
                int newX = x + adjacents[i].Item1;
                int newY = y + adjacents[i].Item2;
                if (InBounds(matrix, x + adjacents[i].Item1, y + adjacents[i].Item2))
                {
                    action.Invoke(matrix[newY][newX], newX, newY);
                }
            }
            //if (InBounds(matrix, x + 1, y))
            //{
            //    action.Invoke(matrix[y][x + 1], x + 1, y);
            //}
            //if (InBounds(matrix, x - 1, y))
            //{
            //    action.Invoke(matrix[y][x - 1], x - 1, y);
            //}
            //if (InBounds(matrix, x, y + 1))
            //{
            //    action.Invoke(matrix[y + 1][x], x, y + 1);
            //}
            //if (InBounds(matrix, x, y - 1))
            //{
            //    action.Invoke(matrix[y - 1][x], x, y - 1);
            //}

            //for (int incY = -1; incY <= 1; incY++)
            //{
            //    for (int incX = -1; incX <= 1; incX++)
            //    {
            //        if ((incX != 0 || incY != 0) && Math.Abs(incX) != Math.Abs(incY) && InBounds(matrix, x + incX, y + incY))
            //        {
            //            action.Invoke(matrix[y + incY][x + incX], x + incX, y + incY);
            //        }
            //    }
            //}
        }

        private static bool InBounds(int[][] matrix, int x, int y)
        {
            return y >= 0
                   && x >= 0
                   && y <= matrix.Length - 1
                   && x <= matrix[y].Length - 1;
        }
    }
}
