using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct PlayerAddMessage : IMessage
{
    public const byte Type = 0x11;
    public required Player[] Players { get; init; }

    internal static PlayerAddMessage Create(short messageLength, CounterBinaryReader reader)
    {
        var players = new List<Player>();
        while (reader.BytesRead < messageLength)
        {
            var playerID = reader.ReadByte();
            var playerName = reader.ReadCString();
            var hash = reader.ReadCString();
            var ip = reader.ReadCString();

            players.Add(new Player
            {
                PlayerID = playerID,
                PlayerName = playerName,
                Hash = hash,
                IP = ip,
            });
        }

        return new PlayerAddMessage
        {
            Players = players.ToArray(),
        };
    }
    public readonly record struct Player
    {
        public required byte PlayerID { get; init; }
        public required string PlayerName { get; init; }
        public required string Hash { get; init; }
        public required string IP { get; init; }
    }
}
