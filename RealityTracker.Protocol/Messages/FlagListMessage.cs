using RealityTracker.Protocol.IO;
using System.Numerics;

namespace RealityTracker.Protocol.Messages;

public readonly record struct FlagListMessage : IMessage
{
    public const byte Type = 0x41;
    public required Flag[] Flags { get; init; }

    internal static FlagListMessage Create(short messageLength, CounterBinaryReader reader)
    {
        var flags = new List<Flag>();
        while (reader.BytesRead < messageLength)
        {
            var id = reader.ReadInt16();
            var team = reader.ReadByte();
            var position = reader.ReadVector3();
            var radius = reader.ReadUInt16();

            flags.Add(new Flag
            {
                Id = id,
                Team = team,
                Position = position,
                Radius = radius
            });
        }

        return new FlagListMessage { Flags = flags.ToArray() };
    }
    public readonly record struct Flag
    {
        public required short Id { get; init; }
        public required byte Team { get; init; }
        public required Vector3 Position { get; init; }
        public required ushort Radius { get; init; }
    }
}
