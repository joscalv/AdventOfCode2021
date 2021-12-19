using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2021
{
    public class Day18 : IDay<long, long>
    {
        private readonly string[] _values;


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

        public static List<int> Reduce(List<int> str)
        {
            var currentStr = str;
            bool isChanged;
            do
            {
                var result = ReduceStep(currentStr);
                isChanged = result.isChange;
                currentStr = result.value;
            } while (isChanged);

            return currentStr;
        }

        public static (List<int> value, bool isChange) ReduceStep(List<int> ints)
        {

            var result = new List<int>();
            int NumberOfOpens = 0;
            int sumToNext = 0;
            bool isChanged;

            isChanged = false;
            for (int i = 0; i < ints.Count; i++)
            {
                if (ints[i] == OpenValue)
                {
                    NumberOfOpens++;
                }

                if (ints[i] == CloseValue)
                {
                    NumberOfOpens--;
                }

                if (!isChanged && NumberOfOpens > 4 && isPair(ints, i))
                {
                    //explode
                    SumToPreviousNumber(result, ints[i + 1]);
                    sumToNext = ints[i + 3];
                    isChanged = true;
                    result.Add(0);
                    i += 4;
                }
                else if (sumToNext > 0 && ints[i] >= 0)
                {
                    result.Add(ints[i] + sumToNext);
                    sumToNext = 0;
                }
                else
                {
                    result.Add(ints[i]);
                }
            }

            if (!isChanged)
            {
                result = new List<int>();
                for (int i = 0; i < ints.Count; i++)
                {
                    if (!isChanged && ints[i] >= 10)
                    {
                        //split
                        result.Add(OpenValue);
                        result.Add((int)ints[i] / 2);
                        result.Add(CommaValue);
                        result.Add((int)Math.Ceiling(ints[i] / 2.0));
                        result.Add(CloseValue);
                        isChanged = true;
                    }
                    else
                    {
                        result.Add(ints[i]);
                    }
                }
            }



            return (result, isChanged);
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

        private static bool isPair(List<int> number, int index)
        {
            return index <= number.Count - 5
                   && number[index] == OpenValue
                   && number[index + 1] >= 0 /*&& number[index + 1] < 10*/
                   && number[index + 2] == CommaValue
                   && number[index + 3] >= 0 /*&& number[index + 3] < 10*/
                   && number[index + 4] == CloseValue;
        }

        private static bool isPairNoLimit(List<int> number, int index)
        {
            return index <= number.Count - 5
                   && number[index] == OpenValue
                   && number[index + 1] >= 0
                   && number[index + 2] == CommaValue
                   && number[index + 3] >= 0
                   && number[index + 4] == CloseValue;
        }

        private const int OpenValue = -1;
        private const int CloseValue = -2;
        private const int CommaValue = -3;


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
                    if (isPairNoLimit(current, i))
                    {
                        result.Add(3 * current[i + 1] + 2 * current[i + 3]);
                        i = i + 4;
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
                    //index++;
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