using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    public readonly record struct RallyAddMessage : IMessage
    {
        public const byte Type = 0x60;

        public byte Team { get; init; }
        public byte Squad { get; init; }
        public Vector3 Position { get; init; }
    }
}
