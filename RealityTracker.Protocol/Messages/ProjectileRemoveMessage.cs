namespace RealityTracker.Protocol.Messages
{
    internal readonly record struct ProjectileRemoveMessage : IMessage
    {
        internal const byte Type = 0x92;

        public ushort Id { get; init; }
    }
}
