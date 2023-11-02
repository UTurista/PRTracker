using RealityTracker.Protocol.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace RealityTracker.Protocol.Messages
{
    public sealed record ServerDetailsMessage : IMessage
    {
        public const byte Type = 0x00;
        public required int Version { get; init; }
        public required float TimePerTick { get; init; }
        public required string IP_Port { get; init; }
        public required string ServerName { get; init; }
        public required byte MaxPlayers { get; init; }
        public required short RoundLength { get; init; }
        public required short BriefingTime { get; init; }
        public required string MapName { get; init; }
        public required string MapGamemode { get; init; }
        public required byte MapLayer { get; init; }
        public required string OpforTeamName { get; init; }
        public required string BluforTeamName { get; init; }
        public required int StartTime { get; init; }
        public required ushort OpforTickets { get; init; }
        public required ushort BluforTickets { get; init; }
        public required  float MapSize { get; init; }
    }
}
