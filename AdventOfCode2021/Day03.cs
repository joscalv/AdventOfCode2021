
namespace AdventOfCode2021
{

    public class Day03 : IDay<int, int>
    {
        private List<byte[]> _values;

        public Day03()
        {
            _values = File
                .ReadAllLines(Path.Combine("Inputs", "input03.txt"))
                .Select(line => line.ToCharArray().ToList().Select(c => (byte)(c - '0')).ToArray())
                .ToList();
        }

        public int ExecutePart1()
        {
            int lineLength = _values[0].Length;
            int[] ones = new int[lineLength];
            int[] zeros = new int[lineLength];

            for (var i = 0; i < _values.Count; i++)
            {
                for (var charIndex = 0; charIndex < lineLength; charIndex++)
                {
                    var charAtI = _values[i][charIndex];
                    if (charAtI == 1)
                    {
                        ones[charIndex] = ones[charIndex] + 1;
                    }
                    else
                    {
                        zeros[charIndex] = zeros[charIndex] + 1;
                    }
                }
            }
            var gamma = new byte[lineLength];
            var epsilon = new byte[lineLength];
            for (var i = 0; i < lineLength; i++)
            {
                gamma[i] = (byte)(ones[i] > zeros[1] ? 1 : 0);
                epsilon[i] = (byte)(ones[i] < zeros[1] ? 1 : 0);
            }
            return ConvertToNumber(gamma) * ConvertToNumber(epsilon);
        }

        public int ExecutePart2()
        {
            int lineLength = _values[0].Length;
            List<byte[]> oxygenValues = _values;
            List<byte[]> co2Values = _values;
            for (var i = 0; i < lineLength; ++i)
            {
                oxygenValues = FilterByMostOrLeastRepeatedAtPosition(oxygenValues, i, true);
                co2Values = co2Values.Count > 1 ? FilterByMostOrLeastRepeatedAtPosition(co2Values, i, false) : co2Values;
            }

            return ConvertToNumber(oxygenValues.First()) * ConvertToNumber(co2Values.First());
        }

        public static List<byte[]> FilterByMostOrLeastRepeatedAtPosition(List<byte[]> values, int index, bool isMostRepeated)
        {
            List<byte[]> ones = new List<byte[]>();
            List<byte[]> zeros = new List<byte[]>();


            for (var i = 0; i < values.Count; i++)
            {
                var charAtI = values[i][index];
                if (charAtI == 1)
                {
                    ones.Add(values[i]);
                }
                else
                {
                    zeros.Add(values[i]);
                }
            }

            if (isMostRepeated)
            {
                return ones.Count >= zeros.Count ? ones : zeros;
            }
            else
            {
                return ones.Count >= zeros.Count ? zeros : ones;
            }
        }

        public static int ConvertToNumber(byte[] value)
        {
            var result = 0;
            for (var i = 0; i < value.Length; i++)
            {
                result = (result << 1 | (value[i]));
            }
            return result;
        }
    }
}