namespace RealityTracker.Protocol.Messages
{
    public sealed record PlayerAddMessage : IMessage
    {
        public const byte Type = 0x11;
        public required Player[] Players { get; init; }

        public sealed record Player
        {
            public required byte PlayerID { get; init; }
            public required string PlayerName { get; init; }
            public required string Hash { get; init; }
            public required string IP { get; init; }
        }
    }
}
