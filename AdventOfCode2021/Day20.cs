namespace AdventOfCode2021
{
    public class Day20 : IDay<long, long>
    {
        private readonly byte[][] _image;
        private readonly byte[] _program;

        public Day20()
        {
            var input = File
                .ReadAllText(Path.Combine("Inputs", "input20.txt"))
                .Split('\n')
                .ToArray();
            var parsed = Day20Extensions.ParseInput(input);
            _program = parsed.program;
            _image = parsed.image;
        }

        public long ExecutePart1()
        {
            var image = ApplyAlgorithm(_image, _program, 2);

            return image.SelectMany(a => a).Count(c => c == 1);
        }

        public long ExecutePart2()
        {
            var image = ApplyAlgorithm(_image, _program, 50);

            return image.SelectMany(a => a).Count(c => c == 1);
        }


        public static byte[][] ApplyAlgorithm(byte[][] imageInput, byte[] program, int repeats = 1)
        {
            byte[][] result = null;
            byte[][] image = imageInput;
            byte background = 0;
            for (int i = 0; i < repeats; i++)
            {
                var imageWidth = image[0].Length + 4;
                var imageHeight = image.Length + 4;
                result = Enumerable.Range(0, imageHeight).Select(_ => new byte[imageWidth]).ToArray();

                for (int y = 0; y < imageHeight; y++)
                {
                    for (int x = 0; x < imageWidth; x++)
                    {
                        result[y][x] = GetValueFromAdjacents(y - 2, x - 2, image, program, background);
                    }
                }

                image = result;
                background = GetValueFromAdjacents(-1000, -1000, image, program, background);
            }


            return result;
        }

        private static byte GetValueFromAdjacents(int y, int x, byte[][] image, byte[] program, byte background)
        {
            int memoryDirecion = 0;
            for (var yInc = -1; yInc <= 1; yInc++)
            {
                for (var xInc = -1; xInc <= 1; xInc++)
                {
                    memoryDirecion = memoryDirecion << 1 | GetValue(image, x + xInc, y + yInc, background);
                }
            }

            return program[memoryDirecion];
        }

        private static byte GetValue(byte[][] image, int x, int y, byte background)
        {
            if (x >= 0 && x < image[0].Length && y >= 0 && y < image.Length)
            {
                return image[y][x];
            }
            else
            {
                return background;
            }
        }

       
        public static class Day20Extensions
        {
            public static (byte[] program, byte[][] image) ParseInput(string[] input)
            {
                var program = input[0].Select(ParseChar).ToArray();

                var lines = input.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
                byte[][] image = lines.TakeLast(lines.Length - 1).Select(ParseLine).ToArray();
                return (program, image);
            }

            private static byte[] ParseLine(string line) => line.Select(c => c == '#' ? (byte)1 : (byte)0).ToArray();

            private static byte ParseChar(char c) => c == '#' ? (byte)1 : (byte)0;
        }
    }
}