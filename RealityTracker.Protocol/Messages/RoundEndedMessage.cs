namespace RealityTracker.Protocol.Messages
{
    public readonly record struct RoundEndedMessage : IMessage
    {
        public const byte Type = 0xF0;
        public static readonly RoundEndedMessage Instance = new();
    }
}
