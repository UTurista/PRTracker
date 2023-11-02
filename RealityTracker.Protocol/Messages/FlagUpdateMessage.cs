namespace RealityTracker.Protocol.Messages
{
    public readonly record struct FlagUpdateMessage : IMessage
    {
        public const byte Type = 0x40;

        public required short Id { get; init; }
        public required byte NewOwner { get; init; }
    }
}
