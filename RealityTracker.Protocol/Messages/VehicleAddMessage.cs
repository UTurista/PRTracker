namespace RealityTracker.Protocol.Messages
{
    public readonly record struct VehicleAddMessage : IMessage
    {
        public const byte Type = 0x21;
        public required Vehicle[] Vehicles { get; init; }

        public sealed record Vehicle
        {
            public required short VehicleID { get; init; }
            public required string Name { get; init; }
            public required ushort MaxHealth { get; init; }
        }
    }
}
