using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

internal readonly record struct ProjectileRemoveMessage : IMessage
{
    internal const byte Type = 0x92;

    public ushort Id { get; init; }



    internal static ProjectileRemoveMessage Create(CounterBinaryReader reader)
    {
        var id = reader.ReadUInt16();

        return new ProjectileRemoveMessage
        {
            Id = id,
        };
    }

}
