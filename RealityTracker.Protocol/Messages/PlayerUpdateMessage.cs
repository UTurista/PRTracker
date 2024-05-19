using RealityTracker.Protocol.IO;
using System.Numerics;

namespace RealityTracker.Protocol.Messages;

public readonly record struct PlayerUpdateMessage : IMessage
{
    public const byte Type = 0x10;
    public required Player[] Players { get; init; }

    internal static PlayerUpdateMessage Create(short messageLength, CounterBinaryReader _reader)
    {
        var players = new List<Player>();
        while (_reader.BytesRead < messageLength)
        {
            var updateFlags = (Flags)_reader.ReadUInt16();
            var playerId = _reader.ReadByte();

            byte? team = updateFlags.HasFlag(Flags.Team) ? _reader.ReadByte() : null;
            byte? squadOrIsSquadLeader = updateFlags.HasFlag(Flags.Squad) ? _reader.ReadByte() : null;
            short? vehicleID = updateFlags.HasFlag(Flags.Vehicle) ? _reader.ReadInt16() : null;
            string? vehicleSeatName = updateFlags.HasFlag(Flags.Vehicle) && vehicleID >= 0 ? _reader.ReadCString() : null;
            byte? vehicleSeatNumber = updateFlags.HasFlag(Flags.Vehicle) && vehicleID >= 0 ? _reader.ReadByte() : null;
            byte? health = updateFlags.HasFlag(Flags.Health) ? _reader.ReadByte() : null;
            short? score = updateFlags.HasFlag(Flags.Score) ? _reader.ReadInt16() : null;
            short? teamworkScore = updateFlags.HasFlag(Flags.TeamworkScore) ? _reader.ReadInt16() : null;
            short? kills = updateFlags.HasFlag(Flags.Kills) ? _reader.ReadInt16() : null;
            short? deaths = updateFlags.HasFlag(Flags.Deaths) ? _reader.ReadInt16() : null;
            short? ping = updateFlags.HasFlag(Flags.Ping) ? _reader.ReadInt16() : null;
            bool? isAlive = updateFlags.HasFlag(Flags.IsAlive) ? _reader.ReadBoolean() : null;
            bool? isJoining = updateFlags.HasFlag(Flags.IsJoining) ? _reader.ReadBoolean() : null;
            Vector3? position = updateFlags.HasFlag(Flags.Position) ? _reader.ReadVector3() : null;
            short? yawRotation = updateFlags.HasFlag(Flags.Rotation) ? _reader.ReadInt16() : null;
            string? kitName = updateFlags.HasFlag(Flags.Kit) ? _reader.ReadCString() : null;

            players.Add(new Player
            {
                UpdateFlags = updateFlags,
                PlayerID = playerId,
                Team = team,
                SquadOrIsSquadLeader = squadOrIsSquadLeader,
                VehicleID = vehicleID,
                VehicleSeatName = vehicleSeatName,
                VehicleSeatNumber = vehicleSeatNumber,
                Health = health,
                Score = score,
                TeamworkScore = teamworkScore,
                Kills = kills,
                Deaths = deaths,
                Ping = ping,
                IsAlive = isAlive,
                IsJoining = isJoining,
                Position = position,
                YawRotation = yawRotation,
                KitName = kitName,
            });
        }

        return new PlayerUpdateMessage
        {
            Players = players.ToArray(),
        };
    }
    public readonly record struct Player
    {
        public required Flags UpdateFlags { get; init; }
        public required byte PlayerID { get; init; }
        public byte? Team { get; init; }
        public byte? SquadOrIsSquadLeader { get; init; }
        public short? VehicleID { get; init; }
        public string? VehicleSeatName { get; init; }
        public byte? VehicleSeatNumber { get; init; }
        public byte? Health { get; init; }
        public short? Score { get; init; }
        public short? TeamworkScore { get; init; }
        public short? Kills { get; init; }
        public short? Deaths { get; init; }
        public short? Ping { get; init; }
        public bool? IsAlive { get; init; }
        public bool? IsJoining { get; init; }
        public Vector3? Position { get; init; }
        public short? YawRotation { get; init; }
        public string? KitName { get; init; }
    }

    [Flags]
    public enum Flags
    {
        Team = 1,
        Squad = 2,
        Vehicle = 4,
        Health = 8,
        Score = 16,
        TeamworkScore = 32,
        Kills = 64,
        Deaths = 256,
        Ping = 512,
        IsAlive = 2048,
        IsJoining = 4096,
        Position = 8192,
        Rotation = 16384,
        Kit = 32768
    }
}
