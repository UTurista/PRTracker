using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityTracker.Protocol.Messages
{
    internal class ProjectileRemoveMessage : IMessage
    {
        internal const byte Type = 0x92;

        public ushort Id { get; internal set; }
    }
}
