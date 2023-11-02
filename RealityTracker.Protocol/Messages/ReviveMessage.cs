namespace RealityTracker.Protocol.Messages
{
    public readonly record struct ReviveMessage : IMessage
    {
        public const byte Type = 0xA0;

        public byte MedicId { get; init; }
        public byte PatientId { get; init; }
    }
}
