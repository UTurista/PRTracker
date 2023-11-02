using System.Runtime.Serialization;

namespace RealityTracker.Protocol.Exceptions
{
    [Serializable]
    public class InvalidMessageLengthException : TrackerException
    {
        public InvalidMessageLengthException(int messageType, int expectedLength, int actualLength) : base($"While reading message 0x{messageType:X2}, read {actualLength}bytes but expected {expectedLength}bytes")
        {
        }

        protected InvalidMessageLengthException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
