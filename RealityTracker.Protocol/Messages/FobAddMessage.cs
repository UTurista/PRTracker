using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    public sealed record FobAddMessage : IMessage
    {
        public const byte Type = 0x30;

        public required Fob[] Fobs { get; init; }

        public sealed record Fob
        {
            public required int Id { get; set; }
            public required short Team { get; set; }
            public required Vector3 Position { get; set; }
        }
    }
}
