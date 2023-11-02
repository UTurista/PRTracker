namespace RealityTracker.Protocol.Messages
{
    public readonly record struct TickMessage : IMessage
    {
        public const byte Type = 0xF1;

        /// <summary>
        /// Game time since last tick message
        /// </summary>
        public required TimeSpan DeltaTime { get; init; }
    }
}
