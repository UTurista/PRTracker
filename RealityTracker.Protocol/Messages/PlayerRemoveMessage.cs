namespace RealityTracker.Protocol.Messages
{
    public readonly record struct PlayerRemoveMessage : IMessage
    {
        public const byte Type = 0x12;
        public required byte PlayerId { get; init; }
    }
}
