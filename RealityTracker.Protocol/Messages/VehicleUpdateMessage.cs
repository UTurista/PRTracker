using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    public readonly record struct VehicleUpdateMessage : IMessage
    {
        public const byte Type = 0x20;
        public required Vehicle[] Vehicles { get; init; }

        public readonly record struct  Vehicle
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
