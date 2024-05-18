using RealityTracker.Protocol.Exceptions;
using RealityTracker.Protocol.IO;
using RealityTracker.Protocol.Messages;
using System.IO.Compression;

namespace RealityTracker.Protocol;

public sealed class RealityReader
{
    private readonly CounterBinaryReader _reader;

    public RealityReader(Stream reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        _reader = new CounterBinaryReader(new ZLibStream(reader, CompressionMode.Decompress));
    }

    public IEnumerable<IMessage> Read()
    {
        while (true)
        {
            short messageLength;
            try
            {
                messageLength = _reader.ReadInt16();
            }
            catch (EndOfStreamException)
            {
                yield break;
            }

            _reader.ResetCount();
            var messageType = _reader.ReadByte();
            var message = ReadMessage(messageType, messageLength, _reader);

            var bytesRead = _reader.BytesRead;
            if (bytesRead != messageLength)
            {
                throw new InvalidMessageLengthException(messageType, messageLength, bytesRead);
            }

            yield return message;
        }
    }


    private static IMessage ReadMessage(byte messageType, short messageLength, CounterBinaryReader reader)
    {
        return messageType switch
        {
            ServerDetailsMessage.Type => ServerDetailsMessage.Create(reader),
            CombatAreaListMessage.Type => CombatAreaListMessage.Create(messageLength, reader),

            PlayerUpdateMessage.Type => PlayerUpdateMessage.Create(messageLength, reader),
            PlayerAddMessage.Type => PlayerAddMessage.Create(messageLength, reader),
            PlayerRemoveMessage.Type => PlayerRemoveMessage.Create(reader),

            VehicleUpdateMessage.Type => VehicleUpdateMessage.Create(messageLength, reader),
            VehicleAddMessage.Type => VehicleAddMessage.Create(messageLength, reader),
            VehicleRemoveMessage.Type => VehicleRemoveMessage.Create(reader),

            FobAddMessage.Type => FobAddMessage.Create(messageLength, reader),
            FobRemoveMessage.Type => FobRemoveMessage.Create(messageLength, reader),

            FlagUpdateMessage.Type => FlagUpdateMessage.Create(reader),
            FlagListMessage.Type => FlagListMessage.Create(messageLength, reader),

            KillMessage.Type => KillMessage.Create(reader),
            ChatMessage.Type => ChatMessage.Create(reader),

            TicketsMessage.TypeTeam1 => TicketsMessage.Create(1, reader),
            TicketsMessage.TypeTeam2 => TicketsMessage.Create(2, reader),

            RallyAddMessage.Type => RallyAddMessage.Create(reader),
            RallyRemoveMessage.Type => RallyRemoveMessage.Create(reader),

            ReviveMessage.Type => ReviveMessage.Create(reader),
            KitAllocatedMessage.Type => KitAllocatedMessage.Create(reader),
            SquadNameMessage.Type => SquadNameMessage.Create(reader),
            SquadLeaderOrdersMessage.Type => SquadLeaderOrdersMessage.Create(messageLength, reader),

            CacheAddMessage.Type => CacheAddMessage.Create(messageLength, reader),
            CacheRemoveMessage.Type => CacheRemoveMessage.Create(reader),
            CacheRevealMessage.Type => CacheRevealMessage.Create(messageLength, reader),
            IntelChangeMessage.Type => IntelChangeMessage.Create(reader),

            ProjectileAddMessage.Type => ProjectileAddMessage.Create(reader),
            ProjectileUpdateMessage.Type => ProjectileUpdateMessage.Create(messageLength, reader),
            ProjectileRemoveMessage.Type => ProjectileRemoveMessage.Create(reader),

            RoundEndedMessage.Type => RoundEndedMessage.Instance,
            TickMessage.Type => TickMessage.Create(reader),
            _ => throw new NotImplementedException($"Message 0x{messageType:X2} is not implemented"),
        };
    }
}