using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    public class RallyAddMessage : IMessage
    {
        public const byte Type = 0x60;

        public byte Team { get; internal set; }
        public byte Squad { get; internal set; }
        public Vector3 Position { get; internal set; }
    }
}
