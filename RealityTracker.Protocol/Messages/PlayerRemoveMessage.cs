using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct PlayerRemoveMessage : IMessage
{
    public const byte Type = 0x12;
    public required byte PlayerId { get; init; }

    internal static PlayerRemoveMessage Create(CounterBinaryReader reader)
    {
        var playerId = reader.ReadByte();
        return new PlayerRemoveMessage
        {
            PlayerId = playerId
        };
    }
}
