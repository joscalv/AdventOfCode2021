using System.Text;

namespace AdventOfCode2021
{
    public class Day18 : IDay<long, long>
    {
        private readonly string[] _values;

        private const int OpenValue = -1;
        private const int CloseValue = -2;
        private const int CommaValue = -3;

        public Day18()
        {
            _values = File
                .ReadAllText(Path.Combine("Inputs", "input18.txt"))
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
        }


        public long ExecutePart1()
        {
            return ExecutePart1(_values).Item1;
        }

        public static (long, string) ExecutePart1(string[] input)
        {
            List<int> current = input.First().ToInteger();
            for (int i = 1; i < input.Length; i++)
            {
                current = Sum(current, input[i].ToInteger());
                current = Reduce(current);
            }

            return (GetMagnitude(current), current.ConvertToString());
        }

        public long ExecutePart2()
        {
            long maxValue = 0;
            for (int i = 0; i < _values.Length; i++)
            {
                for (int j = 0; j < _values[i].Length; j++)
                {
                    if (i != j)
                    {
                        var current = Sum(_values[i].ToInteger(), _values[j].ToInteger());
                        var result = Reduce(current);
                        var magnitude = GetMagnitude(result);
                        if (magnitude > maxValue)
                        {
                            maxValue = magnitude;
                        }
                    }
                }
            }

            return maxValue;
        }

        public static List<int> Sum(List<int> l1, List<int> l2)
        {
            var result = new List<int>() { OpenValue };
            result.AddRange(l1);
            result.Add(CommaValue);
            result.AddRange(l2);
            result.Add(CloseValue);
            return result;
        }

        public static List<int> Reduce(List<int> number)
        {
            var current = number;
            bool isChanged;
            do
            {
                var result = ReduceStep(current);
                isChanged = result.isChange;
                current = result.value;
            } while (isChanged);

            return current;
        }

        public static (List<int> value, bool isChange) ReduceStep(List<int> number)
        {

            var result = Explode(number, out var isChanged);

            if (!isChanged)
            {
                result = Split(number, out isChanged);
            }

            return (result, isChanged);
        }

        private static List<int> Split(List<int> number, out bool isChanged)
        {
            isChanged = false;
            var result = new List<int>();
            for (int i = 0; i < number.Count; i++)
            {
                if (!isChanged && number[i] >= 10)
                {
                    result.Add(OpenValue);
                    result.Add((int)number[i] / 2);
                    result.Add(CommaValue);
                    result.Add((int)Math.Ceiling(number[i] / 2.0));
                    result.Add(CloseValue);
                    isChanged = true;
                }
                else
                {
                    result.Add(number[i]);
                }
            }

            return result;
        }

        private static List<int> Explode(List<int> number, out bool isChanged)
        {
            var result = new List<int>();
            int numberOfOpens = 0;
            int sumToNext = 0;
            isChanged = false;

            for (int i = 0; i < number.Count; i++)
            {
                if (number[i] == OpenValue)
                {
                    numberOfOpens++;
                }

                if (number[i] == CloseValue)
                {
                    numberOfOpens--;
                }

                if (!isChanged && numberOfOpens > 4 && IsPair(number, i))
                {
                    SumToPreviousNumber(result, number[i + 1]);
                    sumToNext = number[i + 3];
                    isChanged = true;
                    result.Add(0);
                    i += 4;
                }
                else if (sumToNext > 0 && number[i] >= 0)
                {
                    result.Add(number[i] + sumToNext);
                    sumToNext = 0;
                }
                else
                {
                    result.Add(number[i]);
                }
            }

            return result;
        }

        private static void SumToPreviousNumber(List<int> result, int i)
        {
            var isNumber = false;
            int index = result.Count - 2;
            while (index >= 0 && !isNumber)
            {
                if (result[index] >= 0)
                {
                    isNumber = true;
                    result[index] += i;
                }

                index--;
            }
        }

        private static bool IsPair(List<int> number, int index)
        {
            return index <= number.Count - 5
                   && number[index] == OpenValue
                   && number[index + 1] >= 0
                   && number[index + 2] == CommaValue
                   && number[index + 3] >= 0
                   && number[index + 4] == CloseValue;
        }

        public static long GetMagnitude(List<int> values)
        {
            var current = values;
            bool changed;
            do
            {
                changed = false;
                var result = new List<int>();
                for (int i = 0; i < current.Count; i++)
                {
                    if (IsPair(current, i))
                    {
                        result.Add(3 * current[i + 1] + 2 * current[i + 3]);
                        i += 4;
                        changed = true;
                    }
                    else
                    {
                        result.Add(current[i]);
                    }
                }

                current = result;
            } while (changed);

            return current.Count > 1 ? -1 : current.First();

        }
    }

    public static class Day18Extensions
    {

        private const int OpenValue = -1;
        private const int CloseValue = -2;
        private const int CommaValue = -3;

        public static List<int> ToInteger(this string number)
        {
            var previousNumber = false;
            List<int> result = new List<int>();
            foreach (var c in number)
            {
                var v = ToInteger(c);

                if (v.isNumber && previousNumber)
                {
                    var lastITem = result.Count - 1;
                    result[lastITem] = result[lastITem] * 10 + v.value;
                }
                else
                {
                    result.Add(v.value);
                }
                previousNumber = v.isNumber;
            }

            return result;
        }

        private static (int value, bool isNumber) ToInteger(char c) => c switch
        {
            '[' => (OpenValue, false),
            ']' => (CloseValue, false),
            ',' => (CommaValue, false),
            _ => (c - '0', true)
        };

        public static string ConvertToString(this List<int> ints)
        {
            var r = ints.Aggregate(new StringBuilder(), (s, v) => s.Append(ToString(v)));
            return r.ToString();
        }

        private static string ToString(int value) => value switch
        {
            OpenValue => "[",
            CloseValue => "]",
            CommaValue => ",",
            _ => $"{value}"
        };
    }
}