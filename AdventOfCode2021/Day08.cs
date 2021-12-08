namespace AdventOfCode2021
{
    public record Entry08(string[] Inputs, string[] Outputs);

    public class Day08 : IDay<long, long>
    {
        private readonly List<Entry08> _values;

        public Day08()
        {
            _values = File
                .ReadAllText(Path.Combine("Inputs", "input08.txt")).ParseInputDay08();
        }

        public long ExecutePart1()
        {
            return ExecutePart1(_values);
        }

        public static long ExecutePart1(List<Entry08> input)
        {
            return input.SelectMany(i => i.Outputs).Count(output => output.Length is 2 or 4 or 3 or 7);
        }

        public long ExecutePart2()
        {
            return ExecutePart2(_values);
        }

        public static long ExecutePart2(List<Entry08> values)
        {
            var result = 0L;
            foreach (var value in values)
            {
                var numbersIdentified = Day08Extensions.IdentifyNumbers(value.Inputs);
                result += Day08Extensions.FixDisplayView(numbersIdentified, value.Outputs);
            }
            return result;
        }
    }


    public static class Day08Extensions
    {
        public static List<Entry08> ParseInputDay08(this string input)
        {
            return input
                .ReplaceLineEndings()
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.ReplaceLineEndings().Split('|'))
                .Select(s => (new Entry08(s[0].Split(' ', StringSplitOptions.RemoveEmptyEntries), s[1].Split(' ', StringSplitOptions.RemoveEmptyEntries))))
                .ToList();
        }

        private static readonly Dictionary<int, DigitalNumber> _values = new()
        {
            { 0, new DigitalNumber(0, "abcefg", new[] { 1, 7 }) },
            { 1, new DigitalNumber(0, "cf", Array.Empty<int>()) },
            { 2, new DigitalNumber(0, "acdeg", Array.Empty<int>()) },
            { 3, new DigitalNumber(0, "acdfg", new[] { 7, 1 }) },
            { 4, new DigitalNumber(0, "bcdf", new[] { 1 }) },
            { 5, new DigitalNumber(0, "abdfg", Array.Empty<int>()) },
            { 6, new DigitalNumber(0, "abdefg", new[] { 5 }) },
            { 7, new DigitalNumber(0, "acf", new[] { 1 }) },
            { 8, new DigitalNumber(0, "abcdefg", new[] { 0, 1, 2, 3, 4, 5, 6, 7, 9 }) },
            { 9, new DigitalNumber(0, "abcdfg", new[] { 2, 4, 3, 5 }) }
        };

        public static Dictionary<string, int> IdentifyNumbers(string[] values)
        {
            // identify unique numbers -> 1,4,7,8
            string one = values.First(v => v.Length == _values[1].Length);

            string four = values.First(v => v.Length == _values[4].Length);
            string seven = values.First(v => v.Length == _values[7].Length);
            string eight = values.First(v => v.Length == _values[8].Length);

            // identify childs 3, 9,6,0
            string zero = values.First(v =>
                v.Length == _values[0].Length && !IsChild(v, four) && (IsChild(v, one) || IsChild(v, seven)));

            string three = values.First(v =>
                v.Length == _values[3].Length && (IsChild(v, one) || IsChild(v, seven)));

            string nine = values.First(v =>
                v.Length == _values[9].Length && v != zero && (IsChild(v, four) || IsChild(v, three) || IsChild(v, one) || IsChild(v, seven)));

            string six = values.First(v =>
                v.Length == _values[6].Length && !IsChild(v, one));

            // identify 2 and 5
            string five = values.First(v =>
                v.Length == _values[5].Length &&
                IsChild(six, v));

            string two = values.First(v =>
                v.Length == _values[2].Length && five != v && !IsChild(v, one));

            var result = new Dictionary<string, int>
            {
                { OrderSegment(zero), 0 },
                { OrderSegment(one), 1 },
                { OrderSegment(two), 2 },
                { OrderSegment(three), 3 },
                { OrderSegment(four), 4 },
                { OrderSegment(five), 5 },
                { OrderSegment(six), 6 },
                { OrderSegment(seven), 7 },
                { OrderSegment(eight), 8 },
                { OrderSegment(nine), 9 },
            };

            return result;

        }

        public static string OrderSegment(string value)
        {
            return new string(value.ToCharArray().OrderBy(c => c).ToArray());
        }

        public static int FixDisplayView(Dictionary<string, int> conversions, string[] digits)
        {
            var result = 0;

            foreach (var digit in digits)
            {
                result *= 10;
                if (conversions.TryGetValue(OrderSegment(digit), out var value))
                {
                    result += value;
                }
            }

            return result;
        }

        private static bool IsChild(string child, string? parent)
        {
            return parent != null && parent.ToCharArray().All(child.Contains);
        }
    }

    public class DigitalNumber
    {
        public int Number { get; }
        public string Segments { get; }
        public int[] Parents { get; }
        public int Length => _length;
        private readonly int _length;
        public DigitalNumber(int number, string segments, int[] parents)
        {

            Number = number;
            Segments = segments;
            Parents = parents;
            _length = segments?.Length ?? 0;
        }


    }

}