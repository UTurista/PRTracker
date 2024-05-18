using RealityTracker.Protocol.IO;
using System.Numerics;

namespace RealityTracker.Protocol.Messages;

internal readonly record struct ProjectileAddMessage : IMessage
{
    internal const byte Type = 0x91;

    public required Vector3 Position { get; init; }
    public required short Yaw { get; init; }
    public required string Name { get; init; }
    public required ushort Id { get; init; }
    public required byte ProjectileType { get; init; }
    public byte PlayerId { get; init; }

    internal static ProjectileAddMessage Create(CounterBinaryReader _reader)
    {
        var id = _reader.ReadUInt16();
        var playerId = _reader.ReadByte();
        var type = _reader.ReadByte();
        var name = _reader.ReadCString();
        var yaw = _reader.ReadInt16();
        var position = _reader.ReadVector3();

        return new ProjectileAddMessage
        {
            Id = id,
            PlayerId = playerId,
            ProjectileType = type,
            Name = name,
            Yaw = yaw,
            Position = position,
        };
    }
}
