using RealityTracker.Protocol.IO;
using System.Numerics;

namespace RealityTracker.Protocol.Messages;

public readonly record struct RallyAddMessage : IMessage
{
    public const byte Type = 0x60;

    public byte Team { get; init; }
    public byte Squad { get; init; }
    public Vector3 Position { get; init; }

    internal static RallyAddMessage Create(CounterBinaryReader reader)
    {
        var teamSquadEncoded = reader.ReadByte();
        var position = reader.ReadVector3();

        return new RallyAddMessage
        {
            Team = teamSquadEncoded,
            Squad = teamSquadEncoded,
            Position = position
        };
    }
}
