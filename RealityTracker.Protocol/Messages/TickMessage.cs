using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct TickMessage : IMessage
{
    public const byte Type = 0xF1;

    /// <summary>
    /// Game time since last tick message
    /// </summary>
    public required TimeSpan DeltaTime { get; init; }


    internal static TickMessage Create(CounterBinaryReader reader)
    {
        var deltaTime = reader.ReadByte();

        return new TickMessage
        {
            DeltaTime = TimeSpan.FromSeconds(deltaTime),
        };
    }
}
