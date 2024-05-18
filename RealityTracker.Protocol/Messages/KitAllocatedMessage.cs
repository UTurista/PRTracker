using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct KitAllocatedMessage : IMessage
{
    public const byte Type = 0xA1;

    public required byte PlayerId { get; init; }
    public required string KitName { get; init; }

    internal static KitAllocatedMessage Create(CounterBinaryReader reader)
    {
        var playerId = reader.ReadByte();
        var kitName = reader.ReadCString();

        return new KitAllocatedMessage
        {
            PlayerId = playerId,
            KitName = kitName,
        };
    }
}
