namespace RealityTracker.Protocol.Messages
{
    public readonly record struct VehicleRemoveMessage : IMessage
    {
        public const byte Type = 0x22;
        public required short VehicleID { get; init; }
        public required bool IsKillerKnown { get; init; }
        public required byte KillerID { get; init; }
    }
}
