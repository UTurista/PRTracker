namespace RealityTracker.Protocol.Messages
{
    public class CacheRemoveMessage : IMessage
    {
        public const byte Type = 0x71;
        public required byte CacheId { get; init; }
    }
}
