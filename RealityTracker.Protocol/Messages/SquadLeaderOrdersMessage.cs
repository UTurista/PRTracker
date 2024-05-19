using RealityTracker.Protocol.IO;
using System.Numerics;

namespace RealityTracker.Protocol.Messages;

public readonly record struct SquadLeaderOrdersMessage : IMessage
{
    public const byte Type = 0xA3;
    public required Order[] Orders { get; init; }


    internal static SquadLeaderOrdersMessage Create(short messageLength, CounterBinaryReader reader)
    {
        var orders = new List<Order>();
        while (reader.BytesRead < messageLength)
        {
            var teamSquad = reader.ReadByte();
            var orderType = reader.ReadByte();
            var position = reader.ReadVector3();

            orders.Add(new Order
            {
                Team = teamSquad,
                Squad = teamSquad,
                Position = position,
            });
        }

        return new SquadLeaderOrdersMessage()
        {
            Orders = orders.ToArray(),
        };
    }

    public readonly record struct Order
    {
        public required byte Team { get; init; }
        public required byte Squad { get; init; }
        public OrderType Type { get; init; }
        public required Vector3 Position { get; init; }
    }
    public enum OrderType
    {

    }
}
