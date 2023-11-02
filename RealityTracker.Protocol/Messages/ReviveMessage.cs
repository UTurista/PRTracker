namespace RealityTracker.Protocol.Messages
{
    public class ReviveMessage : IMessage
    {
        public const byte Type = 0xA0;

        public byte MedicId { get; internal set; }
        public byte PatientId { get; internal set; }
    }
}
