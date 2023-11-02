using RealityTracker.Protocol.Extensions;
using RealityTracker.Protocol.IO;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    public class VehicleUpdateMessage : IMessage
    {
        public const byte Type = 0x20;
        public required Vehicle[] Vehicles { get; init; }

        public sealed record Vehicle
        {
            public required Flags VehicleUpdateFlags { get; init; }
            public required short VehicleID { get; init; }
            public byte? Team { get; init; }
            public Vector3? Position { get; init; }
            public short? YawRotation { get; init; }
            public short? Health { get; init; }
        }

        [Flags]
        public enum Flags
        {
            Team = 1,
            Position = 2,
            Rotation = 4,
            Health = 8,
        }
    }
}
