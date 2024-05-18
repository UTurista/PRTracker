using RealityTracker.Protocol.IO;
using System.Numerics;

namespace RealityTracker.Protocol.Messages;

public readonly record struct FobAddMessage : IMessage
{
    public const byte Type = 0x30;

    public required Fob[] Fobs { get; init; }

    internal static FobAddMessage Create(short messageLength, CounterBinaryReader reader)
    {
        var fobs = new List<FobAddMessage.Fob>();
        while (reader.BytesRead < messageLength)
        {
            var id = reader.ReadInt32();
            var team = reader.ReadByte();
            var position = reader.ReadVector3();

            fobs.Add(new FobAddMessage.Fob
            {
                Id = id,
                Team = team,
                Position = position
            });
        }

        return new FobAddMessage()
        {
            Fobs = fobs.ToArray(),
        };
    }
    public readonly record struct Fob
    {
        public required int Id { get; init; }
        public required short Team { get; init; }
        public required Vector3 Position { get; init; }
    }
}
