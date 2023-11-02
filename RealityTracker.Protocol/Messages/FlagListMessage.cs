using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    public readonly record struct FlagListMessage : IMessage
    {
        public const byte Type = 0x41;
        public required Flag[] Flags { get; init; }

        public readonly record struct Flag
        {
            public required short Id { get; init; }
            public required byte Team { get; init; }
            public required Vector3 Position { get; init; }
            public required ushort Radius { get; init; }
        }
    }
}
