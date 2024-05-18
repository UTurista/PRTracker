using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct VehicleAddMessage : IMessage
{
    public const byte Type = 0x21;
    public required Vehicle[] Vehicles { get; init; }

    internal static VehicleAddMessage Create(short messageLength, CounterBinaryReader reader)
    {
        var vehicles = new List<VehicleAddMessage.Vehicle>();
        while (reader.BytesRead < messageLength)
        {
            var vehicleID = reader.ReadInt16();
            var name = reader.ReadCString();
            var maxHealth = reader.ReadUInt16();

            vehicles.Add(new VehicleAddMessage.Vehicle
            {
                VehicleID = vehicleID,
                Name = name,
                MaxHealth = maxHealth,
            });
        }

        return new VehicleAddMessage()
        {
            Vehicles = vehicles.ToArray(),
        };
    }

    public sealed record Vehicle
    {
        public required short VehicleID { get; init; }
        public required string Name { get; init; }
        public required ushort MaxHealth { get; init; }
    }
}
