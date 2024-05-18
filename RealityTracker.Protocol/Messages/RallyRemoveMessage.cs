using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct RallyRemoveMessage : IMessage
{
    public const byte Type = 0x61;

    public byte Team { get; init; }
    public byte Squad { get; init; }

    internal static RallyRemoveMessage Create(CounterBinaryReader reader)
    {
        var teamSquadEncoded = reader.ReadByte();

        return new RallyRemoveMessage
        {
            Team = teamSquadEncoded,
            Squad = teamSquadEncoded
        };
    }
}
