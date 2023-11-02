namespace RealityTracker.Protocol.Messages
{
    public class CacheRevealMessage : IMessage
    {
        public const byte Type = 0x72;
        public required byte[] Ids { get; init; }
    }
}
