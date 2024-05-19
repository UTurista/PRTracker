using RealityTracker.Protocol.IO;
using System.Numerics;

namespace RealityTracker.Protocol.Messages;

public readonly record struct CacheAddMessage : IMessage
{
    public const byte Type = 0x70;

    public required Cache[] Caches { get; init; }

    internal static CacheAddMessage Create(short messageLength, CounterBinaryReader reader)
    {
        var caches = new List<Cache>();
        while (reader.BytesRead < messageLength)
        {
            var cacheId = reader.ReadByte();
            var position = reader.ReadVector3();
            caches.Add(new Cache
            {
                Id = cacheId,
                Position = position,
            });
        }

        return new CacheAddMessage()
        {
            Caches = caches.ToArray(),
        };
    }
    public sealed record Cache
    {
        public required byte Id { get; init; }
        public required Vector3 Position { get; init; }
    }
}
