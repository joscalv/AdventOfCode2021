namespace AdventOfCode2021
{
    public class Day17 : IDay<int, int>
    {
        private readonly (int xMin, int xMax) _xLimits;
        private readonly (int yMin, int yMax) _yLimits;

        public Day17()
        {
            var text = File
                .ReadAllText(Path.Combine("Inputs", "input17.txt"));
            int indexOfX = text?.IndexOf("=") ?? 0;
            int indexOfY = text?.IndexOf("=", indexOfX + 1) ?? 0;
            var xValue = text.Substring(indexOfX + 1, text.IndexOf(",", indexOfX) - indexOfX - 1).Split("..").Select(int.Parse).ToArray();
            var yValue = text.Substring(indexOfY + 1).Split("..").Select(int.Parse).ToArray();
            _xLimits = (xValue[0], xValue[1]);
            _yLimits = (yValue[0], yValue[1]);

        }

        public int ExecutePart1()
        {
            return CheckTrajectories(_xLimits, _yLimits).maxY;
        }

        public int ExecutePart2()
        {
            return CheckTrajectories(_xLimits, _yLimits).numberOfSolutions;
        }

        public static (int maxY, int numberOfSolutions) CheckTrajectories((int xMin, int xMax) xLimits, (int yMin, int yMax) yLimits)
        {

            var maxSpeedY = Math.Max(Math.Abs(yLimits.yMin), Math.Abs(yLimits.yMax));
            var minSpeedX = (int)Math.Sqrt(2 * xLimits.xMin);
            
            var maxYTrajectory = int.MinValue;
            var numberOfSolutions = 0;

            for (int vx = minSpeedX; vx <= xLimits.xMax; vx++)
            {
                for (int vy = yLimits.yMin; vy <= maxSpeedY; vy++)
                {
                    var checkResult = CheckMatchTarget(vx, vy, xLimits, yLimits);
                    if (checkResult.Item1)
                    {
                        numberOfSolutions++;
                        if (checkResult.maxY > maxYTrajectory)
                        {
                            maxYTrajectory = checkResult.maxY;
                        }
                    }
                }
            }
            return (maxYTrajectory, numberOfSolutions);
        }


        public static (bool, int maxY) CheckMatchTarget(int vx, int vy, (int xMin, int xMax) xLimits, (int yMin, int yMax) yLimits)
        {
            bool found;
            bool isViable = true;
            var x = 0;
            var y = 0;
            var currentVx = vx;
            var currentVy = vy;
            var maxY = int.MinValue;
            do
            {
                x += currentVx;
                y += currentVy;
                if (y > maxY)
                {
                    maxY = y;
                }
                currentVx = currentVx > 0 ? currentVx - 1 : 0;
                currentVy -= 1;
                found = x >= xLimits.xMin && x <= xLimits.xMax && y >= yLimits.yMin && y <= yLimits.yMax;
                isViable &= !(currentVx <= 0 && x < xLimits.xMin) && y >= yLimits.yMin;

            } while (!found && isViable);
            return (found, maxY);
        }
    }
}