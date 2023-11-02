using System.Diagnostics.CodeAnalysis;

namespace RealityTracker.Protocol.Messages
{
    public sealed class TickMessage : IMessage
    {
        public const byte Type = 0xF1;

        /// <summary>
        /// Game time since last tick message
        /// </summary>
        public required TimeSpan DeltaTime { get; init; }
    }
}
