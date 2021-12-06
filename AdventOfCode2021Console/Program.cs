using AdventOfCode2021;
using System.Diagnostics;

namespace AdventOfCode2021Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("*** AdventOfCode2021 ***");
            int day = 0;
            ExecuteDay(new Day01(), ++day, "Sonar Sweep");
            ExecuteDay(new Day02(), ++day, "Dive!");
            ExecuteDay(new Day03(), ++day, "Binary Diagnostic");
            ExecuteDay(new Day04(), ++day, "Giant Squid");
            ExecuteDay(new Day05(), ++day, "Hydrothermal Venture");
            ExecuteDay(new Day06(), ++day, "Lanternfish");

        }

        private static void ExecuteDay<T1, T2>(IDay<T1, T2> day, int dayNumber, string title, string titleProblem1 = "1", string titleProblem2 = "2")
        {
            ExecuteSolution(GetTitle(dayNumber, title, titleProblem1), day.ExecutePart1);
            ExecuteSolution(GetTitle(dayNumber, title, titleProblem2), day.ExecutePart2);
        }

        private static void ExecuteSolution<T>(string title, Func<T> solution)
        {
            ExecuteSolution(title, () => solution?.Invoke()?.ToString() ?? "");
        }

        private static void ExecuteSolution(string title, Func<string> solution)
        {
            Stopwatch clock = new Stopwatch();
            clock.Start();
            var result = solution.Invoke().ToString();
            clock.Stop();
            string separator = "|";

            Console.WriteLine($"{title.PadRight(20)}{separator}{result.PadLeft(15)}{separator}{CalculateMilliseconds(clock).PadLeft(10)}{separator}");
        }

        private static string GetTitle(int dayNumber, string title, string titleProblem)
        {
            return $"Day {dayNumber.ToString().PadRight(2)} {title.PadRight(20)}{titleProblem.PadRight(3)}";
        }

        private static string CalculateMilliseconds(Stopwatch stopwatch)
        {
            return ($"{(1000 * stopwatch.ElapsedTicks / (double)Stopwatch.Frequency):F2}ms");
        }
    }
}