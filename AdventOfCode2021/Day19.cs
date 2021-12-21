using System.Data;
using System.Xml.Serialization;

namespace AdventOfCode2021
{
    public class Day19 : IDay<long, long>
    {
        private static readonly List<Transformation> _transformations;
        private readonly Dictionary<int, List<Coordinate>> _coordinates;

        public record Coordinate(int X, int Y, int Z);

        public record Transformation(char XAxis, char YAxis, char ZAxis, int XDir, int YDir, int ZDir)
        {
            public Coordinate Transform(Coordinate c)
            {
                return c with
                {
                    X = getCoordinate(c, XAxis, XDir),
                    Y = getCoordinate(c, YAxis, YDir),
                    Z = getCoordinate(c, ZAxis, ZDir),
                };
            }
        }

        public record Translation(int X, int Y, int Z)
        {
            public Coordinate Translate(Coordinate c)
            {
                return c with
                {
                    X = c.X - X,
                    Y = c.Y - Y,
                    Z = c.Z - Z,
                };
            }
        }



        static Day19()
        {
            _transformations = new List<Transformation>();
            List<(char xAxis, char yAxis, char zAxis)> transformations = new()
            {
                ('x', 'y', 'z'),
                ('x', 'z', 'y'),
                //('y', 'x', 'z'),
                ('y', 'z', 'x'),
                //('z', 'x', 'y'),
                //('z', 'y', 'x'),

            };
            var coords = new List<int> { -1, 1 };

            foreach (var transformation in transformations)
            {
                foreach (var x in coords)
                {
                    foreach (var y in coords)
                    {
                        foreach (var z in coords)
                        {
                            _transformations.Add(new Transformation(transformation.xAxis, transformation.yAxis, transformation.zAxis, x, y, z));
                        }
                    }
                }
            }
        }

        public Day19()
        {
            var lines = File
                .ReadAllText(Path.Combine("Inputs", "input19.txt"))
                .Split('\n')
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToArray();
            _coordinates = Day19Extensions.ParseInput(lines);



        }


        public long ExecutePart1()
        {
            return CalculateDay19(_coordinates).beacons;
        }

        public long ExecutePart2()
        {
            return CalculateDay19(_coordinates).manhathanDistance;
        }

        public static (int beacons,long manhathanDistance) CalculateDay19(Dictionary<int, List<Coordinate>> coordinatesPerScanner)
        {

            List<(int, List<Coordinate>)> result = new List<(int, List<Coordinate>)>() { (0, coordinatesPerScanner[0]) };
            List<(int, List<Coordinate>)> pending = coordinatesPerScanner
                .Where(kvp => kvp.Key != 0)
                .Select(kvp => (kvp.Key, kvp.Value))
                .ToList();
            List<Coordinate> scanners = new List<Coordinate>(){new Coordinate(0, 0, 0)};

            var lastKey = 0;
            bool found;
            do
            {
                found = false;
                for (int i = result.Count-1; i >= 0 && !found; i--)
                {
                    var r = result[i];
                    foreach (var candidate in pending.ToList())
                    {
                        var hasCommon = HasCommonBeacons(r.Item2, candidate.Item2);
                        if (hasCommon.result)
                        {
                            result.Add((candidate.Item1, hasCommon.coords));
                            pending.Remove(candidate);
                            found = true;
                            scanners.Add(hasCommon.scanner);
                            break;

                        }
                    }
                }
            } while (found == true);

            HashSet<Coordinate> hs = new();
            foreach (var r in result)
            {

                foreach (var coordinate in r.Item2)
                {
                    hs.Add(coordinate);
                }
            }

            long manhathan = 0;
            foreach (var s1 in scanners)
            {
                foreach (var s2 in scanners)
                {
                    var distance= GetManhatan(s1,s2);
                    if (distance > manhathan)
                    {
                        manhathan = distance;
                    }
                }
            }

            return (hs.Count,manhathan) ;
        }

        public static (bool result, List<Coordinate> coords, List<Coordinate> intersection, Coordinate scanner) HasCommonBeacons(List<Coordinate> coords1, List<Coordinate> coords2, int minCommonBeacons = 12)
        {
            int checks = 0;
            var max = 0;
            List<Coordinate> intersection = new();
            var c1Hs = coords1.ToHashSet();
            foreach (var transformation in Day19Extensions.Matrix)
            {
                foreach (var t2 in Day19Extensions.Matrix2)
                {
                    var coords2Transformed = coords2.Select(c => Day19Extensions.Transform(c, transformation, t2)).ToArray();

                    foreach (var c1 in coords1)
                    {
                        foreach (var c2 in coords2Transformed)
                        {
                            checks++;
                            var translation = new Translation(c2.X - c1.X, c2.Y - c1.Y, c2.Z - c1.Z);

                            int coincidences = 0;
                            int index = 0;
                            int length = coords2Transformed.Length;
                            intersection.Clear();
                            while (coincidences < minCommonBeacons && index < length && (minCommonBeacons - coincidences) <= (length - index))
                            {
                                var translated = translation.Translate(coords2Transformed[index]);
                                if (c1Hs.Contains(translated))
                                {
                                    coincidences++;
                                    intersection.Add(translated);
                                }

                                if (coincidences >= minCommonBeacons)
                                {
                                    var scanner = translation.Translate(new Coordinate(0, 0, 0));
                                    return (true, coords2Transformed.Select(c => translation.Translate(c)).ToList(), intersection, scanner);
                                }

                                index++;
                            }
                        }
                    }
                }
            }

            return (false, null, null, null);
        }

        private static int GetManhatan(Coordinate c1, Coordinate c2)
        {
            return Math.Abs(c1.X - c2.X) + Math.Abs(c1.Y - c2.Y) + Math.Abs(c1.Z - c2.Z);
        }

        private static int getCoordinate(Coordinate c, char axis, int direction) => axis switch
        {
            'x' => c.X * direction,
            'y' => c.Y * direction,
            'z' => c.Z * direction,
            _ => throw new NotImplementedException("not supoorted coordinate"),
        };

        public static class Day19Extensions
        {
            public static Dictionary<int, List<Coordinate>> ParseInput(string[] lines)
            {
                Dictionary<int, List<Coordinate>> coordinates = new();
                var scannerId = -1;
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    if (line.Contains("scanner"))
                    {
                        scannerId++;
                        coordinates.Add(scannerId, new List<Coordinate>());
                    }
                    else
                    {
                        var c = ParseCoordinate(line);
                        coordinates[scannerId].Add(c);
                    }
                }

                return coordinates;
            }

            public static Coordinate ParseCoordinate(string str)
            {
                var values = str.Split(',', StringSplitOptions.RemoveEmptyEntries);
                return new Coordinate(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
            }

            public static Coordinate Transform(Coordinate c, int[,] matrix, int[] matrix2)
            {
                var x = (c.X * matrix[0, 0] + c.Y * matrix[0, 1] + c.Z * matrix[0, 2]) * matrix2[0];
                var y = (c.X * matrix[1, 0] + c.Y * matrix[1, 1] + c.Z * matrix[1, 2]) * matrix2[1];
                var z = (c.X * matrix[2, 0] + c.Y * matrix[2, 1] + c.Z * matrix[2, 2]) * matrix2[2];

                return new Coordinate(x, y, z);
            }

            public static List<int[,]> Matrix = new List<int[,]>()
            {
                new int[,] { { 1, 0, 0 },{0, 1, 0}, { 0, 0, 1 } },
                new int[,] { { 1, 0, 0 },{0, 0, 1}, { 0, 1, 0 } },
                new int[,] { { 0, 1, 0 },{1, 0, 0}, { 0, 0, 1 } },
                new int[,] { { 0, 1, 0 },{0, 0, 1}, { 1, 0, 0 } },
                new int[,] { { 0, 0, 1 },{1, 0, 0}, { 0, 1, 0 } },
                new int[,] { { 0, 0, 1 },{0, 1, 0}, { 1, 0, 0 } },
            };

            public static List<int[]> Matrix2 = new List<int[]>()
            {
                new[] { 1, 1, 1 },
                new[] { 1, 1, -1 },
                new[] { 1, -1, 1 },
                new[] { 1, -1, -1 },
                new[] { -1, 1, 1 },
                new[] { -1, 1, -1 },
                new[] { -1, -1, 1 },
                new[] { -1, -1, -1 },
            };


        }
    }

}