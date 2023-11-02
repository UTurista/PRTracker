using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    public sealed record CacheAddMessage : IMessage
    {
        public const byte Type = 0x70;

        public required Cache[] Caches { get; init; }

        public sealed record Cache
        {
            public required byte Id { get; init; }
            public required Vector3 Position { get; init; }
        }
    }
}
