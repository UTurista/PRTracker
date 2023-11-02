using RealityTracker.Protocol.Extensions;
using RealityTracker.Protocol.IO;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    public class PlayerUpdateMessage : IMessage
    {
        public const byte Type = 0x10;
        public required Player[] Players { get; init; }

        public sealed record Player
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
}
