using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct ReviveMessage : IMessage
{
    public const byte Type = 0xA0;

    public byte MedicId { get; init; }
    public byte PatientId { get; init; }

    internal static ReviveMessage Create(CounterBinaryReader reader)
    {
        var medicId = reader.ReadByte();
        var patientId = reader.ReadByte();

        return new ReviveMessage
        {
            MedicId = medicId,
            PatientId = patientId
        };
    }
}
