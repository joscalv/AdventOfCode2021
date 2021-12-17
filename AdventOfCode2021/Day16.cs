namespace AdventOfCode2021
{

    public class Day16 : IDay<long, long>
    {
        private readonly byte[] _input;

        public Day16()
        {
            var str = File.ReadAllText(Path.Combine("Inputs", "input16.txt"))
                .ReplaceLineEndings()
                .Replace(Environment.NewLine, "");
            _input = PacketReader.ToBinary(str);
        }

        public long ExecutePart1()
        {
            return PacketReader.ParsePacket(_input).GetVersionSum();
        }

        public long ExecutePart2()
        {
            return PacketReader.ParsePacket(_input).GetValue();
        }
    }

    public static class PacketReader
    {
        private const uint HeaderLength = 6;
        private const uint MinPacketLength = 10;
        private const uint OperatorId0Length = 15;
        private const uint OperatorId1Length = 11;

        public static Packet ParsePacket(byte[] input)
        {
            return ParsePackets(input, out _).First();
        }

        public static List<Packet> ParsePackets(byte[] input, out uint endIndex, uint start = 0,
            uint? packetsToRead = null, uint? lengthToRead = null)
        {
            List<Packet> result = new();
            uint index = start;
            uint packetsRead = 0;

            while (input.Length - index >= MinPacketLength
                   && (lengthToRead == null || (index - start < lengthToRead))
                   && (packetsToRead == null || packetsRead < packetsToRead))
            {
                var version = ReadValue(input, index + 0, 3);
                var type = ReadValue(input, index + 3, 3);

                var currentPacket = new Packet(version, type);

                index += HeaderLength;

                if (currentPacket.Type == PacketType.Literal)
                {
                    long value = 0;
                    uint firstValue;
                    do
                    {
                        firstValue = ReadValue(input, index++, 1);
                        value = (value << 4) + ReadValue(input, index, 4);
                        index += 4;
                    } while (firstValue != 0);

                    currentPacket.Value = value;
                }
                else
                {

                    currentPacket.LengthType = (LengthType)ReadValue(input, index, 1);
                    index++;
                    if (currentPacket.LengthType == LengthType.TotalLength)
                    {
                        currentPacket.Length = ReadValue(input, index, OperatorId0Length);
                        index += OperatorId0Length;
                        var packets = ParsePackets(input, out var currentEndIndex, index, null, currentPacket.Length);
                        currentPacket.Packets = packets;
                        index = currentEndIndex;
                    }
                    else
                    {
                        currentPacket.Length = ReadValue(input, index, OperatorId1Length);
                        index += OperatorId1Length;
                        var packets = ParsePackets(input, out var currentEndIndex, index, currentPacket.Length);
                        currentPacket.Packets = packets;
                        index = currentEndIndex;
                    }
                }
                result.Add(currentPacket);
                packetsRead++;
            }
            endIndex = index;

            return result;
        }

        public static uint ReadValue(byte[] data, uint start, uint length)
        {
            uint value = 0;
            for (uint i = start; i < start + length; i++)
            {
                value = value << 1 | data[i];
            }

            return value;
        }

        public static byte[] ToBinary(string value)
        {
            return value.ToCharArray().Select(GetHexVal).SelectMany(IntToBinary).ToArray();
        }

        public static IEnumerable<byte> IntToBinary(byte value)
        {
            yield return (byte)(value >> 3 & 1);
            yield return (byte)(value >> 2 & 1);
            yield return (byte)(value >> 1 & 1);
            yield return (byte)(value & 1);
        }

        public static byte GetHexVal(char hex) => (byte)(hex - (hex < 58 ? 48 : 55));
    }

    public class Packet
    {
        public long Version { get; set; }
        public uint TypeId { get; set; }
        public uint Length { get; set; }
        public PacketType Type => TypeId == 4 ? PacketType.Literal : PacketType.Operator;
        public PacketSubType SubType => (PacketSubType)TypeId;
        public long Value { get; set; }
        public LengthType LengthType { get; set; }
        public List<Packet> Packets { get; set; }

        public Packet(uint version, uint typeId)
        {
            Version = version;
            TypeId = typeId;
            Packets = new List<Packet>();
        }

        public long GetVersionSum()
        {
            return Version + (Packets?.Any() == true ? Packets.Select(p => p.GetVersionSum()).Sum() : 0);
        }

        public long GetValue()
        {
            return SubType switch
            {
                PacketSubType.Sum => Packets.Select(p => p.GetValue()).Sum(),
                PacketSubType.Multiply => Packets.Aggregate(1L, (acc, p) => acc * p.GetValue()),
                PacketSubType.Min => Packets.Select(p => p.GetValue()).Min(),
                PacketSubType.Max => Packets.Select(p => p.GetValue()).Max(),
                PacketSubType.Literal => Value,
                PacketSubType.GreaterThan => Packets[0].GetValue() > Packets[1].GetValue() ? 1 : 0,
                PacketSubType.LessThan => Packets[0].GetValue() < Packets[1].GetValue() ? 1 : 0,
                PacketSubType.EqualTo => Packets[0].GetValue() == Packets[1].GetValue() ? 1 : 0,
                _ => throw new NotSupportedException()
            };
        }
    }

    public enum LengthType
    {
        TotalLength = 0,
        SubPackets = 1
    }

    public enum PacketType
    {
        Literal,
        Operator
    }

    public enum PacketSubType
    {
        Sum = 0,
        Multiply = 1,
        Min = 2,
        Max = 3,
        Literal = 4,
        GreaterThan = 5,
        LessThan = 6,
        EqualTo = 7,
    }
}