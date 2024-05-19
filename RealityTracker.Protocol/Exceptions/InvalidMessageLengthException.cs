namespace RealityTracker.Protocol.Exceptions;

public sealed class InvalidMessageLengthException : TrackerException
{
    public InvalidMessageLengthException(int messageType, int expectedLength, int actualLength) : base($"While reading message 0x{messageType:X2}, read {actualLength}bytes but expected {expectedLength}bytes")
    {
    }
}
