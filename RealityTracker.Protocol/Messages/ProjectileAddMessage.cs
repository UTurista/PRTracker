using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    internal readonly record struct ProjectileAddMessage : IMessage
    {
        internal const byte Type = 0x91;

        public required Vector3 Position { get; init; }
        public required short Yaw { get; init; }
        public required string Name { get; init; }
        public required ushort Id { get; init; }
        public required byte ProjectileType { get; init; }
        public byte PlayerId { get; init; }
    }
}
