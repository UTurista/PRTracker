using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct FlagUpdateMessage : IMessage
{
    public const byte Type = 0x40;

    public required short Id { get; init; }
    public required byte NewOwner { get; init; }

    internal static FlagUpdateMessage Create(CounterBinaryReader reader)
    {
        var id = reader.ReadInt16();
        var newOwner = reader.ReadByte();

        return new FlagUpdateMessage
        {
            Id = id,
            NewOwner = newOwner,
        };
    }
}
