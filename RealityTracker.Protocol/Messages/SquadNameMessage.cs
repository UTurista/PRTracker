namespace RealityTracker.Protocol.Messages
{
    public readonly record struct SquadNameMessage : IMessage
    {
        public const byte Type = 0xA2;

        public required byte Team { get; init; }
        public required byte Squad { get; init; }
        public required string Message { get; init; }
    }
}
