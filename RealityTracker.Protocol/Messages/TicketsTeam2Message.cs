using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct TicketsMessage : IMessage
{
    public const byte TypeTeam1 = 0x52;
    public const byte TypeTeam2 = 0x53;

    public short Tickets { get; init; }
    public byte Team { get; init; }

    internal static TicketsMessage Create(byte team, CounterBinaryReader reader)
    {
        var tickets = reader.ReadInt16();
        return new TicketsMessage
        {
            Tickets = tickets,
            Team = team,
        };
    }
}
