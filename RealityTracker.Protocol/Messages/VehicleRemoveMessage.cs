using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct VehicleRemoveMessage : IMessage
{
    public const byte Type = 0x22;
    public required short VehicleID { get; init; }
    public required bool IsKillerKnown { get; init; }
    public required byte KillerID { get; init; }

    internal static VehicleRemoveMessage Create(CounterBinaryReader reader)
    {
        var vehicleID = reader.ReadInt16();
        var isKillerKnown = reader.ReadBoolean();
        var killerID = reader.ReadByte();

        return new VehicleRemoveMessage
        {
            VehicleID = vehicleID,
            IsKillerKnown = isKillerKnown,
            KillerID = killerID,
        };
    }
}
