namespace RealityTracker.Protocol.Messages
{
    public class IntelChangeMessage : IMessage
    {
        public const byte Type = 0x73;

        public required sbyte Intel { get; init; }
    }
}
