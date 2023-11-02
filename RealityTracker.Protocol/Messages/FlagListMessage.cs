using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    public class FlagListMessage : IMessage
    {
        public const byte Type = 0x41;
        public required Flag[] Flags { get; init; }

        public class Flag
        {
            public required short Id { get; set; }
            public required byte Team { get; set; }
            public required Vector3 Position { get; set; }
            public required ushort Radius { get; set; }
        }
    }
}
