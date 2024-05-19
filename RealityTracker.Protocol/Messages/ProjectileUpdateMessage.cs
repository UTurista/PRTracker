using RealityTracker.Protocol.IO;
using System.Numerics;

namespace RealityTracker.Protocol.Messages;

internal readonly record struct ProjectileUpdateMessage : IMessage
{
    internal const byte Type = 0x90;

    public Projectile[] Projectiles { get; init; }

    internal static ProjectileUpdateMessage Create(short messageLength, CounterBinaryReader reader)
    {
        var projectiles = new List<Projectile>();
        while (reader.BytesRead < messageLength)
        {
            var id = reader.ReadUInt16();
            var yaw = reader.ReadInt16();
            var position = reader.ReadVector3();

            projectiles.Add(new Projectile
            {
                Id = id,
                Yaw = yaw,
                Position = position
            });
        }

        return new ProjectileUpdateMessage()
        {
            Projectiles = projectiles.ToArray(),
        };
    }
    internal readonly record struct Projectile
    {
        public Vector3? Position { get; init; }
        public ushort Id { get; init; }
        public short Yaw { get; init; }
    }
}
