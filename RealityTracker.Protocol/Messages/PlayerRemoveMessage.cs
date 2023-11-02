using RealityTracker.Protocol.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace RealityTracker.Protocol.Messages
{
    public class PlayerRemoveMessage : IMessage
    {
        public const byte Type = 0x12;
        public required byte PlayerId { get; init; }
    }
}
