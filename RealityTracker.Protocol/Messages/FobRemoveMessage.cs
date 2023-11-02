namespace RealityTracker.Protocol.Messages
{
    public readonly record struct FobRemoveMessage : IMessage
    {
        public const byte Type = 0x31;
        public required int[] Ids { get; init; }
    }
}
