using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RealityTracker.Protocol.Messages
{
    internal class ProjectileUpdateMessage : IMessage
    {
        internal const byte Type = 0x90;

        public Projectile[] Projectiles { get; internal set; }

        internal class Projectile
        {
            public Vector3? Position { get; set; }
            public ushort Id { get; internal set; }
            public short Yaw { get; internal set; }
        }
    }
}
