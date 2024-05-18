using RealityTracker.Protocol.IO;
using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    public readonly record struct VehicleUpdateMessage : IMessage
    {
        public const byte Type = 0x20;
        public required Vehicle[] Vehicles { get; init; }

        internal static VehicleUpdateMessage Create(short messageLength, CounterBinaryReader reader)
        {
            var vehicles = new List<VehicleUpdateMessage.Vehicle>();
            while (reader.BytesRead < messageLength)
            {
                var vehicleUpdateFlags = (VehicleUpdateMessage.Flags)reader.ReadByte();
                var vehicleID = reader.ReadInt16();
                byte? team = vehicleUpdateFlags.HasFlag(VehicleUpdateMessage.Flags.Team) ? reader.ReadByte() : null;
                Vector3? position = vehicleUpdateFlags.HasFlag(VehicleUpdateMessage.Flags.Position) ? reader.ReadVector3() : null;
                short? yawRotation = vehicleUpdateFlags.HasFlag(VehicleUpdateMessage.Flags.Rotation) ? reader.ReadInt16() : null;
                short? health = vehicleUpdateFlags.HasFlag(VehicleUpdateMessage.Flags.Health) ? reader.ReadInt16() : null;

                vehicles.Add(new VehicleUpdateMessage.Vehicle
                {
                    VehicleUpdateFlags = vehicleUpdateFlags,
                    VehicleID = vehicleID,
                    Team = team,
                    Position = position,
                    YawRotation = yawRotation,
                    Health = health,
                });
            }

            return new VehicleUpdateMessage()
            {
                Vehicles = vehicles.ToArray(),
            };
        }

        public readonly record struct Vehicle
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
