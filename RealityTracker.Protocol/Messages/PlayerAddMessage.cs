namespace RealityTracker.Protocol.Messages
{
    public readonly record struct PlayerAddMessage : IMessage
    {
        public const byte Type = 0x11;
        public required Player[] Players { get; init; }

        public readonly record struct Player
        {
            public required byte PlayerID { get; init; }
            public required string PlayerName { get; init; }
            public required string Hash { get; init; }
            public required string IP { get; init; }
        }
    }
}
