namespace RealityTracker.Protocol.Messages
{
    public class FobRemoveMessage : IMessage
    {
        public const byte Type = 0x31;
        public required int[] Ids { get; init; }
    }
}
