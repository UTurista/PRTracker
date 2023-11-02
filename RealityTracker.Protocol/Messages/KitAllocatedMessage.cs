namespace RealityTracker.Protocol.Messages
{
    public class KitAllocatedMessage : IMessage
    {
        public const byte Type = 0xA1;

        public required byte PlayerId { get; init; }
        public required string KitName { get; init; }
    }
}
