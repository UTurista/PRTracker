using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct FobRemoveMessage : IMessage
{
    public const byte Type = 0x31;
    public required int[] Ids { get; init; }

    internal static FobRemoveMessage Create(short messageLength, CounterBinaryReader reader)
    {
        var ids = new List<int>();
        while (reader.BytesRead < messageLength)
        {
            var id = reader.ReadInt32();
            ids.Add(id);
        }

        return new FobRemoveMessage()
        {
            Ids = ids.ToArray(),

        };
    }
}
