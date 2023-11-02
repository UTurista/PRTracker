using RealityTracker.Protocol.Extensions;
using RealityTracker.Protocol.IO;
using System.Diagnostics.CodeAnalysis;

namespace RealityTracker.Protocol.Messages
{
    public class VehicleRemoveMessage : IMessage
    {
        public const byte Type = 0x22;
        public required short VehicleID { get; set; }
        public required bool IsKillerKnown { get; set; }
        public required byte KillerID { get; set; }
    }
}
