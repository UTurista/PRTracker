namespace RealityTracker.Protocol.Messages
{
    public readonly record struct CacheRevealMessage : IMessage
    {
        public const byte Type = 0x72;
        public required byte[] Ids { get; init; }
    }
}
