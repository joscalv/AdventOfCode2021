using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day16Test
    {

        [Fact]
        public void TestPart1()
        {
            Day16 day = new();
            var expectedSolution = 993;
            day.ExecutePart1().Should().Be(expectedSolution);

        }
        [Fact]
        public void TestPart2()
        {
            Day16 day = new();
            var expectedSolution = 144595909277L;
            day.ExecutePart2().Should().Be(expectedSolution);

        }

        [Fact]
        public void CovertHexStringToBinary()
        {
            var expected = "110100101111111000101000".ToCharArray().Select(c => c - '0').ToArray();
            PacketReader.ToBinary("D2FE28").Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ParsePacketLiteral()
        {
            var binary = PacketReader.ToBinary("D2FE28");
            var packet = PacketReader.ParsePacket(binary);
            packet.Version.Should().Be(6);
            packet.TypeId.Should().Be(4);
            packet.Type.Should().Be(PacketType.Literal);
            packet.Value.Should().Be(2021);
        }

        [Fact]
        public void ParsePacketOperatorLength0()
        {
            var expectedBinary = "00111000000000000110111101000101001010010001001000000000".ToCharArray().Select(c => c - '0').ToArray();

            var binary = PacketReader.ToBinary("38006F45291200");
            binary.Should().BeEquivalentTo(expectedBinary);

            var packet = PacketReader.ParsePacket(binary);
            packet.Version.Should().Be(1);
            packet.TypeId.Should().Be(6);
            packet.Type.Should().Be(PacketType.Operator);
            packet.LengthType.Should().Be(LengthType.TotalLength);
            packet.Length.Should().Be(27);
            packet.Packets.Count.Should().Be(2);
            packet.Packets[0].Type.Should().Be(PacketType.Literal);
            packet.Packets[0].Value.Should().Be(10);
            packet.Packets[1].Type.Should().Be(PacketType.Literal);
            packet.Packets[1].Value.Should().Be(20);
        }

        [Fact]
        public void ParsePacketOperatorLength1()
        {
            var expectedBinary = "11101110000000001101010000001100100000100011000001100000".ToCharArray().Select(c => c - '0').ToArray();

            var binary = PacketReader.ToBinary("EE00D40C823060");
            binary.Should().BeEquivalentTo(expectedBinary);

            var packet = PacketReader.ParsePacket(binary);
            packet.Version.Should().Be(7);
            packet.TypeId.Should().Be(3);
            packet.Type.Should().Be(PacketType.Operator);
            packet.LengthType.Should().Be(LengthType.SubPackets);
            packet.Length.Should().Be(3);
            packet.Packets.Count.Should().Be(3);
            packet.Packets[0].Type.Should().Be(PacketType.Literal);
            packet.Packets[0].Value.Should().Be(1);
            packet.Packets[1].Type.Should().Be(PacketType.Literal);
            packet.Packets[1].Value.Should().Be(2);
            packet.Packets[2].Type.Should().Be(PacketType.Literal);
            packet.Packets[2].Value.Should().Be(3);
        }

        [Fact]
        public void ParsePacketOperator2()
        {
            var binary = PacketReader.ToBinary("8A004A801A8002F478");

            var packet = PacketReader.ParsePacket(binary);
            packet.Version.Should().Be(4);
            packet.Type.Should().Be(PacketType.Operator);
            packet.Packets.Count.Should().Be(1);
            packet.Packets[0].Version.Should().Be(1);
            packet.Packets[0].Type.Should().Be(PacketType.Operator);
            packet.Packets[0].Length.Should().Be(1);
            packet.Packets[0].Packets[0].Version.Should().Be(5);
            packet.Packets[0].Packets[0].Packets[0].Version.Should().Be(6);
            packet.Packets[0].Packets[0].Packets[0].Type.Should().Be(PacketType.Literal);
            packet.GetVersionSum().Should().Be(16);
        }

        [Fact]
        public void ParsePacketOperator3()
        {
            PacketReader.ParsePacket(PacketReader.ToBinary("8A004A801A8002F478")).GetVersionSum().Should().Be(16);
            PacketReader.ParsePacket(PacketReader.ToBinary("620080001611562C8802118E34")).GetVersionSum().Should().Be(12);
            PacketReader.ParsePacket(PacketReader.ToBinary("620080001611562C8802118E34")).GetVersionSum().Should().Be(12);
            PacketReader.ParsePacket(PacketReader.ToBinary("C0015000016115A2E0802F182340")).GetVersionSum().Should().Be(23);
            PacketReader.ParsePacket(PacketReader.ToBinary("A0016C880162017C3686B18A3D4780")).GetVersionSum().Should().Be(31);
        }

        [Fact]
        public void TestSamplePart2()
        {
            PacketReader.ParsePacket(PacketReader.ToBinary("C200B40A82")).GetValue().Should().Be(3);
            PacketReader.ParsePacket(PacketReader.ToBinary("04005AC33890")).GetValue().Should().Be(54);
            PacketReader.ParsePacket(PacketReader.ToBinary("880086C3E88112")).GetValue().Should().Be(7);
            PacketReader.ParsePacket(PacketReader.ToBinary("CE00C43D881120")).GetValue().Should().Be(9);
            PacketReader.ParsePacket(PacketReader.ToBinary("D8005AC2A8F0")).GetValue().Should().Be(1);
            PacketReader.ParsePacket(PacketReader.ToBinary("F600BC2D8F")).GetValue().Should().Be(0);
            PacketReader.ParsePacket(PacketReader.ToBinary("9C005AC2F8F0")).GetValue().Should().Be(0);
            PacketReader.ParsePacket(PacketReader.ToBinary("9C0141080250320F1802104A08")).GetValue().Should().Be(1);

        }
    }
}