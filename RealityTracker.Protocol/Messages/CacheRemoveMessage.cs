namespace RealityTracker.Protocol.Messages
{
    public readonly record struct CacheRemoveMessage : IMessage
    {
        public const byte Type = 0x71;
        public required byte CacheId { get; init; }
    }
}
