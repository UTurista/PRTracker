using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RealityTracker.Protocol.Messages
{
    internal class ProjectileAddMessage : IMessage
    {
        internal const byte Type = 0x91;

        public required Vector3 Position { get; init; }
        public required short Yaw { get; init; }
        public required string Name { get; init; }
        public required ushort Id { get; init; }
        public required byte ProjectileType { get; init; }
        public byte PlayerId { get; internal set; }
    }
}
