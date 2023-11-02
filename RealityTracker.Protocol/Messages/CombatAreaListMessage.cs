namespace RealityTracker.Protocol.Messages
{
    public class CombatAreaListMessage : IMessage
    {
        public const byte Type = 0x01;
        public required Area[] Areas { get; init; }

        public class Area
        {
            public required byte Team { get; init; }
            public required byte Inverted { get; init; }
            public required byte CombatAreaType { get; init; }
            public required byte NumberOfPoints { get; init; }
            public required float[] Points { get; init; }
        }
    }
}
