using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages
{
    public readonly record struct ServerDetailsMessage : IMessage
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
        public required float MapSize { get; init; }

        public static ServerDetailsMessage Create(CounterBinaryReader reader)
        {
            var version = reader.ReadInt32();
            var timePerTick = reader.ReadSingle();
            var iP_Port = reader.ReadCString();
            var serverName = reader.ReadCString();
            var maxPlayers = reader.ReadByte();
            var roundLength = reader.ReadInt16();
            var briefingTime = reader.ReadInt16();
            var mapName = reader.ReadCString();
            var mapGamemode = reader.ReadCString();
            var mapLayer = reader.ReadByte();
            var opforTeamName = reader.ReadCString();
            var bluforTeamName = reader.ReadCString();
            var startTime = reader.ReadInt32();
            var opforTickets = reader.ReadUInt16();
            var bluforTickets = reader.ReadUInt16();
            var mapSize = reader.ReadSingle();

            return new ServerDetailsMessage
            {
                Version = version,
                TimePerTick = timePerTick,
                IP_Port = iP_Port,
                ServerName = serverName,
                MaxPlayers = maxPlayers,
                RoundLength = roundLength,
                BriefingTime = briefingTime,
                MapName = mapName,
                MapGamemode = mapGamemode,
                MapLayer = mapLayer,
                OpforTeamName = opforTeamName,
                BluforTeamName = bluforTeamName,
                StartTime = startTime,
                OpforTickets = opforTickets,
                BluforTickets = bluforTickets,
                MapSize = mapSize,
            };
        }
    }
}
