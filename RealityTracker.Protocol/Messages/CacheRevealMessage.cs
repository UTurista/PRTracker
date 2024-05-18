using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct CacheRevealMessage : IMessage
{
    public const byte Type = 0x72;
    public required byte[] Ids { get; init; }

    internal static CacheRevealMessage Create(short messageLength, CounterBinaryReader reader)
    {
        var ids = new List<byte>();
        while (reader.BytesRead < messageLength)
        {
            var id = reader.ReadByte();

            ids.Add(id);
        }

        return new CacheRevealMessage()
        {
            Ids = ids.ToArray(),
        };
    }
}
