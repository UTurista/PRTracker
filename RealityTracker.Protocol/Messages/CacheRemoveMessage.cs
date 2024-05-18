using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct CacheRemoveMessage : IMessage
{
    public const byte Type = 0x71;
    public required byte CacheId { get; init; }

    internal static CacheRemoveMessage Create(CounterBinaryReader reader)
    {
        var cacheId = reader.ReadByte();
        return new CacheRemoveMessage
        {
            CacheId = cacheId
        };
    }
}
