﻿namespace RealityTracker.Protocol.Messages
{
    public readonly record struct KillMessage : IMessage
    {
        public const byte Type = 0x50;
        public required string Weapon { get; init; }
        public required byte VictimId { get; init; }
        public required byte AttackerId { get; init; }
    }
}
