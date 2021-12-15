namespace AdventOfCode2021
{
    public class Day14 : IDay<long, ulong>
    {
        private readonly Dictionary<RuleKey, char> _rules;
        private readonly char[] _template;

        public Day14()
        {
            var input = File
                .ReadAllText(Path.Combine("Inputs", "input14.txt"));
            var parsedData = Day14Extensions.Parse(input);
            _rules = parsedData.rules;
            _template = parsedData.template;
        }

        public long ExecutePart1()
        {
            return ApplyRules(_rules, _template);
        }

        public ulong ExecutePart2()
        {
            return ApplyRulesOptimized(_rules, _template, 40);
        }
        
        public ulong ExecutePart2v2()
        {
            return ApplyRulesOptimizedv2(_rules, _template, 40);
        }

        public static long ApplyRules(Dictionary<RuleKey, char> rules, char[] template, int loops = 10)
        {
            var currentTemplate = template;
            for (int i = 0; i < loops; i++)
            {
                currentTemplate = ReplaceRules(rules, currentTemplate);
            }

            var counters = currentTemplate.GroupBy(c => c).Select(g => g.Count()).ToArray();
            var min = counters.Min();
            var max = counters.Max();
            return max - min;
        }


        public static ulong ApplyRulesOptimized(Dictionary<RuleKey, char> rules, char[] template, int loops = 10)
        {
            Dictionary<MemoKey, ulong[]> memoization = new();

            var result = new ulong[25];
            for (int i = 0; i < template.Length - 1; i++)
            {
                var currentRuleKey = new RuleKey(template[i], template[i + 1]);
                var current = ExpandSingleRulesNTimes(memoization, rules, currentRuleKey, loops);
                result = SumArrays(result, current);
                if (i > 0)
                {
                    result[GetIndex(currentRuleKey.Item0)] -= 1;
                }
            }

            return result.Max() - result.Where(i => i != 0).Min();
        }

        public static ulong[] ExpandSingleRulesNTimes(Dictionary<MemoKey, ulong[]> memoization,
            Dictionary<RuleKey, char> rules, RuleKey key, int iteration)
        {
            if (memoization.TryGetValue(new MemoKey(key.Item0, key.Item1, iteration), out var values))
            {
                return values;
            }

            if (iteration > 1 && rules.TryGetValue(key, out var replacement))
            {
                var rule1 = key with { Item1 = replacement };
                var resultRule1 = ExpandSingleRulesNTimes(memoization, rules, rule1, iteration - 1);

                var rule2 = key with { Item0 = replacement };
                var resultRule2 = ExpandSingleRulesNTimes(memoization, rules, rule2, iteration - 1);

                var result = SumArrays(resultRule1, resultRule2);
                result[GetIndex(rule2.Item0)] -= 1; //Remove overlap between results

                memoization.Add(new MemoKey(key.Item0, key.Item1, iteration), result);
                return result;
            }

            if (rules.TryGetValue(key, out replacement))
            {
                var result2 = new ulong[25];
                result2[GetIndex(key.Item0)] += 1;
                result2[GetIndex(replacement)] += 1;
                result2[GetIndex(key.Item1)] += 1;

                memoization.Add(new MemoKey(key.Item0, key.Item1, iteration), result2);

                return result2;

            }

            throw new NotSupportedException();
        }

        private static int GetIndex(char c) => c - 'A';

        private static ulong[] SumArrays(ulong[] a, ulong[] b)
        {
            var result = new ulong[25];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = a[i] + b[i];
            }

            return result;
        }

        public static char[] ReplaceRules(Dictionary<RuleKey, char> rules, char[] template)
        {
            List<char> result = new();
            for (int i = 0; i < template.Length - 1; i++)
            {
                var currentRuleKeu = new RuleKey(template[i], template[i + 1]);
                result.Add(template[i]);
                if (rules.TryGetValue(currentRuleKeu, out var replacement))
                {
                    result.Add(replacement);
                }
            }

            result.Add(template.Last());
            return result.ToArray();
        }

        public static ulong ApplyRulesOptimizedv2(Dictionary<RuleKey, char> rules, char[] template, int loops = 10)
        {
            var characters = template.GroupBy(c => c).ToDictionary(g => g.Key, g => (ulong)g.Count());
            var pairs = new Dictionary<(char, char), ulong>();
            for (int i = 0; i < template.Length-1; i++)
            {
                pairs.TryAdd((template[i], template[i + 1]), 0);
                pairs[(template[i], template[i + 1])] += 1;
            }

            for (var i = 0; i < loops; i++)
            {
                var newPairs = new Dictionary<(char, char), ulong>();
                foreach (var pair in pairs)
                {
                    if (rules.TryGetValue(new RuleKey(pair.Key.Item1, pair.Key.Item2), out var newC))
                    {
                        characters.TryAdd(newC, 0);
                        characters[newC] += pair.Value;
                        newPairs.TryAdd((pair.Key.Item1, newC), 0);
                        newPairs.TryAdd((newC, pair.Key.Item2), 0);
                        newPairs[(pair.Key.Item1, newC)] += pair.Value;
                        newPairs[(newC, pair.Key.Item2)] += pair.Value;
                    }
                    else
                    {
                        newPairs.Add((pair.Key.Item1, pair.Key.Item2), pair.Value);
                    }

                }

                pairs = newPairs;
            }

            return characters.Values.Max() - characters.Values.Min();
        }
    }

    public record RuleKey(char Item0, char Item1);
    public record MemoKey(char Item0, char Item1, int Iteration);

    public static class Day14Extensions
    {
        public static (char[] template, Dictionary<RuleKey, char> rules) Parse(string input)
        {
            var lines = input
                .ReplaceLineEndings()
                .Split(Environment.NewLine);

            var template = lines[0].ToCharArray();
            var rules = lines
                .TakeLast(lines.Length - 1)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(ParseRule)
                .ToDictionary(i => i.key, i => i.c);

            return (template, rules);
        }

        public static (RuleKey key, char c) ParseRule(string ruleStr)
        {
            var pair = ruleStr.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
            return (new RuleKey(pair[0][0], pair[0][1]), pair[1][0]);

        }

    }

}