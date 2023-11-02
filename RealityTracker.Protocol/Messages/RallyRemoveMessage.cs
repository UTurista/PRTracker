namespace RealityTracker.Protocol.Messages
{
    public class RallyRemoveMessage : IMessage
    {
        public const byte Type = 0x61;

        public byte Team { get; internal set; }
        public byte Squad { get; internal set; }
    }
}
