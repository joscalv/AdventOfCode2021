using AdventOfCode2021;
using System.Diagnostics;

namespace AdventOfCode2021Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("*** AdventOfCode2021 ***");

            ExecuteDay(new Day01(), 1, "Sonar Sweep");

        }

        private static void ExecuteDay<T1, T2>(IDay<T1, T2> day, int dayNumber, string title, string titleProblem1 = "Part One", string titleProblem2 = "Part Two")
        {
            ExecuteSolution($"Day {dayNumber} - {title} - {titleProblem1}", day.ExecutePart1);
            ExecuteSolution($"Day {dayNumber} - {title} - {titleProblem2}", day.ExecutePart2);
        }

        private static void ExecuteSolution<T>(string title, Func<T> solution)
        {
            ExecuteSolution(title, () =>
            {
                return solution?.Invoke()?.ToString() ?? "";
            });
        }

        private static void ExecuteSolution(string title, Func<string> solution)
        {
            Stopwatch clock = new Stopwatch();
            clock.Start();
            var result = solution.Invoke().ToString();
            clock.Stop();
            string separator = "|";

            Console.WriteLine($"{separator}{title.PadRight(20)}{separator}{result.PadLeft(15)}{separator}{CalculateMilliseconds(clock).PadLeft(10)}{separator}");
        }

        private static string CalculateMilliseconds(Stopwatch stopwatch)
        {
            return ($"{(1000 * stopwatch.ElapsedTicks / (double)Stopwatch.Frequency):F2}ms");
        }
    }
}