using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    public readonly record struct FobAddMessage : IMessage
    {
        public const byte Type = 0x30;

        public required Fob[] Fobs { get; init; }

        public readonly record struct Fob
        {
            public required int Id { get; init; }
            public required short Team { get; init; }
            public required Vector3 Position { get; init; }
        }
    }
}
