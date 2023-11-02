﻿using System.Numerics;

namespace RealityTracker.Protocol.Messages
{
    public class SquadLeaderOrdersMessage : IMessage
    {
        public const byte Type = 0xA3;
        public required Order[] Orders { get; init; }

        public class Order
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
}
