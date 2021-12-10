using System.Text;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day10 : IDay<long, long>
    {
        private readonly string[] _values;

        public Day10()
        {
            _values = File
                .ReadAllText(Path.Combine("Inputs", "input10.txt"))
                .Split('\n')
                .ToArray();
        }

        public long ExecutePart1()
        {
            return _values.Select(Day10Extensions.GetScoreOfCorruptedChar).Sum();
        }

        public long ExecutePart2()
        {
            var list = _values.Select(Day10Extensions.GetFixScoreIfNotCorrupted).Where(n => n != 0).OrderBy(n => n).ToList();
            return list[list.Count / 2];
        }
    }

    public static class Day10Extensions
    {
        public static int GetScoreOfCorruptedChar(string line)
        {
            return IsCorrupted(line, out var errorChar) ? GetScoreOfCorrupted(errorChar) : 0;
        }

        public static bool IsCorrupted(string input, out char error)
        {
            error = (char)0;
            Stack<char> stack = new Stack<char>();

            foreach (char c in input)
            {
                if (IsOpen(c))
                {
                    stack.Push(c);
                }
                else if (IsClose(c) && GetOpenOf(c) != stack.Peek())
                {
                    error = c;
                    return true;
                }
                else
                {
                    stack.Pop();
                }
            }
            return false;
        }

        public static long GetFixScoreIfNotCorrupted(string input)
        {
            Stack<char> stack = new Stack<char>();
            long resultCount = 0;

            foreach (char c in input)
            {
                if (IsOpen(c))
                {
                    stack.Push(c);
                }
                else if (IsClose(c) && GetOpenOf(c) != stack.Peek())
                {
                    return 0;
                }
                else
                {
                    stack.Pop();
                }
            }
            while (stack.Any())
            {
                var c = stack.Pop();
                resultCount = resultCount * 5 + c switch
                {
                    '(' => 1,
                    '[' => 2,
                    '{' => 3,
                    '<' => 4,
                    _ => 0
                };
            }
            return resultCount;
        }

        private static bool IsOpen(char c) => c is '(' or '[' or '{' or '<';

        private static bool IsClose(char c) => c is ')' or ']' or '}' or '>';

        private static int GetScoreOfCorrupted(char c) => c switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
            _ => 0,
        };

        private static int GetOpenOf(char c) => c switch
        {
            ')' => '(',
            _ => c - 2,
        };

    }
}