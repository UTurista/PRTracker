using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    internal readonly record struct ProjectileUpdateMessage : IMessage
    {
        internal const byte Type = 0x90;

        public Projectile[] Projectiles { get; init; }

        internal readonly record struct Projectile
        {
            public Vector3? Position { get; init; }
            public ushort Id { get; init; }
            public short Yaw { get; init; }
        }
    }
}
