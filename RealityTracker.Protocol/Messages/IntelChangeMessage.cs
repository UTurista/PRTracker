using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct IntelChangeMessage : IMessage
{
    public const byte Type = 0x73;

    public required sbyte Intel { get; init; }

    internal static IntelChangeMessage Create(CounterBinaryReader reader)
    {
        sbyte intel = reader.ReadSByte();

        return new IntelChangeMessage
        {
            Intel = intel,
        };
    }

}
