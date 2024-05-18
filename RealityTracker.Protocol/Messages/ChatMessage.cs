using RealityTracker.Protocol.IO;

namespace RealityTracker.Protocol.Messages;

public readonly record struct ChatMessage : IMessage
{
    public const byte Type = 0x51;
    public required ChatChannel Channel { get; init; }
    public byte? Squad { get; init; }
    public required byte PlayerId { get; init; }
    public required string Message { get; init; }

    internal static ChatMessage Create(CounterBinaryReader reader)
    {
        var encodedChannel = reader.ReadByte();
        var playerId = reader.ReadByte();
        var message = reader.ReadCString();
        var (channel, squad) = DecodeChannel(encodedChannel);

        return new ChatMessage
        {
            Channel = channel,
            Squad = squad,
            PlayerId = playerId,
            Message = message,
        };
    }

    private static (ChatMessage.ChatChannel channel, byte? squad) DecodeChannel(byte encodedChannel)
    {
        if (encodedChannel == 0x00)
        {
            return (ChatMessage.ChatChannel.All, null);
        }

        if (encodedChannel >= 0x30)
        {
            return ((ChatMessage.ChatChannel)encodedChannel, null);
        }

        byte squad = (byte)(0x01 & encodedChannel);
        byte channel = (byte)(0x10 & encodedChannel);
        return ((ChatMessage.ChatChannel)channel, squad);
    }

    public enum ChatChannel
    {
        All = 0x00,
        Team1 = 0x10,
        Team2 = 0x20,
        Server = 0x30,
        ServerResponse = 0x31,
        AdminAlert = 0x32,
        ServerMessage = 0x33,
        ServerTeam1 = 0x34,
        ServerTeam2 = 0x35,
    }
}
