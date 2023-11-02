namespace RealityTracker.Protocol.Messages
{
    public class TicketsMessage : IMessage
    {
        public const byte TypeTeam1 = 0x52;
        public const byte TypeTeam2 = 0x53;

        public short Tickets { get; internal set; }
        public byte Team { get; internal set; }
    }
}
