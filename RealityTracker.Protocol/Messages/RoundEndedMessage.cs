namespace RealityTracker.Protocol.Messages
{
    public class RoundEndedMessage : IMessage
    {
        public const byte Type = 0xF0;

        public RoundEndedMessage(BinaryReader reader, short messageLength)
        {
        }
    }
}
