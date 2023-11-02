namespace RealityTracker.Protocol.Messages
{
    public class SquadNameMessage : IMessage
    {
        public const byte Type = 0xA2;

        public required byte Team { get; init; }
        public required byte Squad { get; init; }
        public required string Message { get; init; }
    }
}
