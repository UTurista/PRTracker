using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct SquadNameMessage : IMessage
{
    public const byte Type = 0xA2;

    public required byte Team { get; init; }
    public required byte Squad { get; init; }
    public required string Message { get; init; }

    internal static SquadNameMessage Create(CounterBinaryReader reader)
    {
        var teamSquad = reader.ReadByte();
        var message = reader.ReadCString();

        return new SquadNameMessage()
        {
            Team = teamSquad,
            Squad = teamSquad,
            Message = message,
        };
    }
}
