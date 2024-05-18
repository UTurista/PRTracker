using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct CombatAreaListMessage : IMessage
{
    public const byte Type = 0x01;
    public required Area[] Areas { get; init; }

    internal static CombatAreaListMessage Create(short messageLength, CounterBinaryReader _reader)
    {
        var areas = new List<CombatAreaListMessage.Area>();
        while (_reader.BytesRead < messageLength)
        {
            byte team = _reader.ReadByte();
            byte inverted = _reader.ReadByte();
            byte type = _reader.ReadByte();
            byte nPoints = _reader.ReadByte();
            float[] points = _reader.ReadSingleArray(2 * nPoints);

            areas.Add(new CombatAreaListMessage.Area
            {
                Team = team,
                Inverted = inverted,
                CombatAreaType = type,
                NumberOfPoints = nPoints,
                Points = points,
            });
        }

        return new CombatAreaListMessage
        {
            Areas = areas.ToArray(),
        };
    }

    public readonly record struct Area
    {
        public required byte Team { get; init; }
        public required byte Inverted { get; init; }
        public required byte CombatAreaType { get; init; }
        public required byte NumberOfPoints { get; init; }
        public required float[] Points { get; init; }
    }
}
