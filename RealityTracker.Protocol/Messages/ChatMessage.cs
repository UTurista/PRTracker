namespace RealityTracker.Protocol.Messages
{
    public sealed record ChatMessage : IMessage
    {
        public const byte Type = 0x51;
        public required ChatChannel Channel { get; init; }
        public byte? Squad { get; init; }
        public required byte PlayerId { get; init; }
        public required string Message { get; init; }
        
        public enum ChatChannel
        {
            All = 0x00,
            Team1 = 0x10,
            Team2 = 0x20,
            Server = 0x30,
            ServerResponse = 0x31,
            AdminAlert = 0x32,
            ServerMessage = 0x33,
            ServerTeam1 = 0x34,
            ServerTeam2 = 0x35,
        }
    }
}
