using System.Runtime.Serialization;

namespace RealityTracker.Protocol.Exceptions
{
    [Serializable]
    public class TrackerException : Exception
    {
        public TrackerException(string? message) : base(message)
        {
        }

        public TrackerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TrackerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
