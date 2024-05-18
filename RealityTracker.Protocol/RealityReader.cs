using RealityTracker.Protocol.Exceptions;
using RealityTracker.Protocol.IO;
using RealityTracker.Protocol.Messages;
using System.IO.Compression;
using System.Numerics;

namespace RealityTracker.Protocol
{
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


        private IMessage ReadMessage(byte messageType, short messageLength, CounterBinaryReader _reader)
        {
            return messageType switch
            {
                ServerDetailsMessage.Type => ReadServerDetailsMessage(),
                CombatAreaListMessage.Type => ReadCombatAreaListMessage(messageLength),

                PlayerUpdateMessage.Type => ReadPlayerUpdateMessage(messageLength),
                PlayerAddMessage.Type => ReadPlayerAddMessage(messageLength),
                PlayerRemoveMessage.Type => ReadPlayerRemoveMessage(),

                VehicleUpdateMessage.Type => ReadVehicleUpdateMessage(messageLength),
                VehicleAddMessage.Type => ReadVehicleAddMessage(messageLength),
                VehicleRemoveMessage.Type => ReadVehicleRemoveMessage(),

                FobAddMessage.Type => ReadFobAddMessage(messageLength),
                FobRemoveMessage.Type => ReadFobRemoveMessage(messageLength),

                FlagUpdateMessage.Type => ReadFlagUpdateMessage(),
                FlagListMessage.Type => ReadFlagListMessage(messageLength),

                KillMessage.Type => ReadKillMessage(),
                ChatMessage.Type => ReadChatMessage(),

                TicketsMessage.TypeTeam1 => ReadTicketsMessage(1),
                TicketsMessage.TypeTeam2 => ReadTicketsMessage(2),

                RallyAddMessage.Type => ReadRallyAddMessage(),
                RallyRemoveMessage.Type => ReadRallyRemoveMessage(),

                ReviveMessage.Type => ReadReviveMessage(),
                KitAllocatedMessage.Type => ReadKitAllocatedMessage(),
                SquadNameMessage.Type => ReadSquadNameMessage(),
                SquadLeaderOrdersMessage.Type => ReadSquadLeaderOrdersMessage(messageLength),

                CacheAddMessage.Type => ReadCacheAddMessage(messageLength),
                CacheRemoveMessage.Type => ReadCacheRemoveMessage(),
                CacheRevealMessage.Type => ReadCacheRevealMessage(messageLength),
                IntelChangeMessage.Type => ReadIntelChangeMessage(),

                ProjectileAddMessage.Type => ReadProjectileAddMessage(),
                ProjectileUpdateMessage.Type => ReadProjectileUpdateMessage(messageLength),
                ProjectileRemoveMessage.Type => ReadProjectileRemoveMessage(),

                RoundEndedMessage.Type => RoundEndedMessage.Instance,
                TickMessage.Type => ReadTickMessage(),
                _ => throw new NotImplementedException($"Message 0x{messageType:X2} is not implemented"),
            };
        }

        private FlagUpdateMessage ReadFlagUpdateMessage()
        {
            var id = _reader.ReadInt16();
            var newOwner = _reader.ReadByte();

            return new FlagUpdateMessage
            {
                Id = id,
                NewOwner = newOwner,
            };
        }

        private PlayerRemoveMessage ReadPlayerRemoveMessage()
        {
            var playerId = _reader.ReadByte();
            return new PlayerRemoveMessage
            {
                PlayerId = playerId
            };
        }

        private FobRemoveMessage ReadFobRemoveMessage(short messageLength)
        {
            var ids = new List<int>();
            while (_reader.BytesRead < messageLength)
            {
                var id = _reader.ReadInt32();
                ids.Add(id);
            }

            return new FobRemoveMessage()
            {
                Ids = ids.ToArray(),

            };
        }

        private CacheRemoveMessage ReadCacheRemoveMessage()
        {
            var cacheId = _reader.ReadByte();
            return new CacheRemoveMessage
            {
                CacheId = cacheId
            };
        }

        private RallyRemoveMessage ReadRallyRemoveMessage()
        {
            var teamSquadEncoded = _reader.ReadByte();

            return new RallyRemoveMessage
            {
                Team = teamSquadEncoded,
                Squad = teamSquadEncoded
            };
        }

        private RallyAddMessage ReadRallyAddMessage()
        {
            var teamSquadEncoded = _reader.ReadByte();
            var position = _reader.ReadVector3();

            return new RallyAddMessage
            {
                Team = teamSquadEncoded,
                Squad = teamSquadEncoded,
                Position = position
            };
        }

        private ReviveMessage ReadReviveMessage()
        {
            var medicId = _reader.ReadByte();
            var patientId = _reader.ReadByte();

            return new ReviveMessage
            {
                MedicId = medicId,
                PatientId = patientId
            };
        }

        private CacheRevealMessage ReadCacheRevealMessage(short messageLength)
        {
            var ids = new List<byte>();
            while (_reader.BytesRead < messageLength)
            {
                var id = _reader.ReadByte();

                ids.Add(id);
            }

            return new CacheRevealMessage()
            {
                Ids = ids.ToArray(),
            };
        }

        private KillMessage ReadKillMessage()
        {
            var attackerId = _reader.ReadByte();
            var victimId = _reader.ReadByte();
            var weapon = _reader.ReadCString();

            return new KillMessage
            {
                AttackerId = attackerId,
                VictimId = victimId,
                Weapon = weapon,
            };
        }

        private VehicleRemoveMessage ReadVehicleRemoveMessage()
        {
            var vehicleID = _reader.ReadInt16();
            var isKillerKnown = _reader.ReadBoolean();
            var killerID = _reader.ReadByte();

            return new VehicleRemoveMessage
            {
                VehicleID = vehicleID,
                IsKillerKnown = isKillerKnown,
                KillerID = killerID,
            };
        }

        private FobAddMessage ReadFobAddMessage(short messageLength)
        {
            var fobs = new List<FobAddMessage.Fob>();
            while (_reader.BytesRead < messageLength)
            {
                var id = _reader.ReadInt32();
                var team = _reader.ReadByte();
                var position = _reader.ReadVector3();

                fobs.Add(new FobAddMessage.Fob
                {
                    Id = id,
                    Team = team,
                    Position = position
                });
            }

            return new FobAddMessage()
            {
                Fobs = fobs.ToArray(),
            };
        }

        private TicketsMessage ReadTicketsMessage(byte team)
        {
            var tickets = _reader.ReadInt16();
            return new TicketsMessage
            {
                Tickets = tickets,
                Team = team,
            };
        }

        private ProjectileRemoveMessage ReadProjectileRemoveMessage()
        {
            var id = _reader.ReadUInt16();

            return new ProjectileRemoveMessage
            {
                Id = id,
            };
        }

        private ProjectileUpdateMessage ReadProjectileUpdateMessage(short messageLength)
        {
            var projectiles = new List<ProjectileUpdateMessage.Projectile>();
            while (_reader.BytesRead < messageLength)
            {
                var id = _reader.ReadUInt16();
                var yaw = _reader.ReadInt16();
                var position = _reader.ReadVector3();

                projectiles.Add(new ProjectileUpdateMessage.Projectile
                {
                    Id = id,
                    Yaw = yaw,
                    Position = position
                });
            }

            return new ProjectileUpdateMessage()
            {
                Projectiles = projectiles.ToArray(),
            };
        }

        private ProjectileAddMessage ReadProjectileAddMessage()
        {
            var id = _reader.ReadUInt16();
            var playerId = _reader.ReadByte();
            var type = _reader.ReadByte();
            var name = _reader.ReadCString();
            var yaw = _reader.ReadInt16();
            var position = _reader.ReadVector3();

            return new ProjectileAddMessage
            {
                Id = id,
                PlayerId = playerId,
                ProjectileType = type,
                Name = name,
                Yaw = yaw,
                Position = position,
            };
        }

        private TickMessage ReadTickMessage()
        {
            var deltaTime = _reader.ReadByte();

            return new TickMessage
            {
                DeltaTime = TimeSpan.FromSeconds(deltaTime),
            };
        }

        private VehicleUpdateMessage ReadVehicleUpdateMessage(short messageLength)
        {
            var vehicles = new List<VehicleUpdateMessage.Vehicle>();
            while (_reader.BytesRead < messageLength)
            {
                var vehicleUpdateFlags = (VehicleUpdateMessage.Flags)_reader.ReadByte();
                var vehicleID = _reader.ReadInt16();
                byte? team = vehicleUpdateFlags.HasFlag(VehicleUpdateMessage.Flags.Team) ? _reader.ReadByte() : null;
                Vector3? position = vehicleUpdateFlags.HasFlag(VehicleUpdateMessage.Flags.Position) ? _reader.ReadVector3() : null;
                short? yawRotation = vehicleUpdateFlags.HasFlag(VehicleUpdateMessage.Flags.Rotation) ? _reader.ReadInt16() : null;
                short? health = vehicleUpdateFlags.HasFlag(VehicleUpdateMessage.Flags.Health) ? _reader.ReadInt16() : null;

                vehicles.Add(new VehicleUpdateMessage.Vehicle
                {
                    VehicleUpdateFlags = vehicleUpdateFlags,
                    VehicleID = vehicleID,
                    Team = team,
                    Position = position,
                    YawRotation = yawRotation,
                    Health = health,
                });
            }

            return new VehicleUpdateMessage()
            {
                Vehicles = vehicles.ToArray(),
            };
        }

        private VehicleAddMessage ReadVehicleAddMessage(short messageLength)
        {
            var vehicles = new List<VehicleAddMessage.Vehicle>();
            while (_reader.BytesRead < messageLength)
            {
                var vehicleID = _reader.ReadInt16();
                var name = _reader.ReadCString();
                var maxHealth = _reader.ReadUInt16();

                vehicles.Add(new VehicleAddMessage.Vehicle
                {
                    VehicleID = vehicleID,
                    Name = name,
                    MaxHealth = maxHealth,
                });
            }

            return new VehicleAddMessage()
            {
                Vehicles = vehicles.ToArray(),
            };
        }

        private KitAllocatedMessage ReadKitAllocatedMessage()
        {
            var playerId = _reader.ReadByte();
            var kitName = _reader.ReadCString();

            return new KitAllocatedMessage
            {
                PlayerId = playerId,
                KitName = kitName,
            };
        }

        private SquadLeaderOrdersMessage ReadSquadLeaderOrdersMessage(short messageLength)
        {
            var orders = new List<SquadLeaderOrdersMessage.Order>();
            while (_reader.BytesRead < messageLength)
            {
                var teamSquad = _reader.ReadByte();
                var orderType = _reader.ReadByte();
                var position = _reader.ReadVector3();

                orders.Add(new SquadLeaderOrdersMessage.Order
                {
                    Team = teamSquad,
                    Squad = teamSquad,
                    Position = position,
                });
            }

            return new SquadLeaderOrdersMessage()
            {
                Orders = orders.ToArray(),
            };
        }

        private CacheAddMessage ReadCacheAddMessage(short messageLength)
        {
            var caches = new List<CacheAddMessage.Cache>();
            while (_reader.BytesRead < messageLength)
            {
                var cacheId = _reader.ReadByte();
                var position = _reader.ReadVector3();
                caches.Add(new CacheAddMessage.Cache
                {
                    Id = cacheId,
                    Position = position,
                });
            }

            return new CacheAddMessage()
            {
                Caches = caches.ToArray(),
            };
        }

        private SquadNameMessage ReadSquadNameMessage()
        {
            var teamSquad = _reader.ReadByte();
            var message = _reader.ReadCString();

            return new SquadNameMessage()
            {
                Team = teamSquad,
                Squad = teamSquad,
                Message = message,
            };
        }

        private ChatMessage ReadChatMessage()
        {
            var encodedChannel = _reader.ReadByte();
            var playerId = _reader.ReadByte();
            var message = _reader.ReadCString();
            var (channel, squad) = DecodeChannel(encodedChannel);

            return new ChatMessage
            {
                Channel = channel,
                Squad = squad,
                PlayerId = playerId,
                Message = message,
            };
        }

        private (ChatMessage.ChatChannel channel, byte? squad) DecodeChannel(byte encodedChannel)
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

        private CombatAreaListMessage ReadCombatAreaListMessage(short messageLength)
        {
            var areas = new List<CombatAreaListMessage.Area>();
            while (_reader.BytesRead < messageLength)
            {
                byte team = _reader.ReadByte();
                byte inverted = _reader.ReadByte();
                byte type = _reader.ReadByte();
                byte nPoints = _reader.ReadByte();
                float[] points = _reader.ReadSingleArray(2 * nPoints);

                areas.Add(new CombatAreaListMessage.Area
                {
                    Team = team,
                    Inverted = inverted,
                    CombatAreaType = type,
                    NumberOfPoints = nPoints,
                    Points = points,
                });
            }

            return new CombatAreaListMessage
            {
                Areas = areas.ToArray(),
            };
        }

        private PlayerUpdateMessage ReadPlayerUpdateMessage(short messageLength)
        {
            var players = new List<PlayerUpdateMessage.Player>();
            while (_reader.BytesRead < messageLength)
            {
                var updateFlags = (PlayerUpdateMessage.Flags)_reader.ReadUInt16();
                var playerId = _reader.ReadByte();

                byte? team = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Team) ? _reader.ReadByte() : null;
                byte? squadOrIsSquadLeader = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Squad) ? _reader.ReadByte() : null;
                short? vehicleID = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Vehicle) ? _reader.ReadInt16() : null;
                string? vehicleSeatName = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Vehicle) && vehicleID >= 0 ? _reader.ReadCString() : null;
                byte? vehicleSeatNumber = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Vehicle) && vehicleID >= 0 ? _reader.ReadByte() : null;
                byte? health = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Health) ? _reader.ReadByte() : null;
                short? score = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Score) ? _reader.ReadInt16() : null;
                short? teamworkScore = updateFlags.HasFlag(PlayerUpdateMessage.Flags.TeamworkScore) ? _reader.ReadInt16() : null;
                short? kills = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Kills) ? _reader.ReadInt16() : null;
                short? deaths = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Deaths) ? _reader.ReadInt16() : null;
                short? ping = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Ping) ? _reader.ReadInt16() : null;
                bool? isAlive = updateFlags.HasFlag(PlayerUpdateMessage.Flags.IsAlive) ? _reader.ReadBoolean() : null;
                bool? isJoining = updateFlags.HasFlag(PlayerUpdateMessage.Flags.IsJoining) ? _reader.ReadBoolean() : null;
                Vector3? position = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Position) ? _reader.ReadVector3() : null;
                short? yawRotation = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Rotation) ? _reader.ReadInt16() : null;
                string? kitName = updateFlags.HasFlag(PlayerUpdateMessage.Flags.Kit) ? _reader.ReadCString() : null;

                players.Add(new PlayerUpdateMessage.Player
                {
                    UpdateFlags = updateFlags,
                    PlayerID = playerId,
                    Team = team,
                    SquadOrIsSquadLeader = squadOrIsSquadLeader,
                    VehicleID = vehicleID,
                    VehicleSeatName = vehicleSeatName,
                    VehicleSeatNumber = vehicleSeatNumber,
                    Health = health,
                    Score = score,
                    TeamworkScore = teamworkScore,
                    Kills = kills,
                    Deaths = deaths,
                    Ping = ping,
                    IsAlive = isAlive,
                    IsJoining = isJoining,
                    Position = position,
                    YawRotation = yawRotation,
                    KitName = kitName,
                });
            }

            return new PlayerUpdateMessage
            {
                Players = players.ToArray(),
            };
        }

        private PlayerAddMessage ReadPlayerAddMessage(short messageLength)
        {
            var players = new List<PlayerAddMessage.Player>();
            while (_reader.BytesRead < messageLength)
            {
                var playerID = _reader.ReadByte();
                var playerName = _reader.ReadCString();
                var hash = _reader.ReadCString();
                var ip = _reader.ReadCString();

                players.Add(new PlayerAddMessage.Player
                {
                    PlayerID = playerID,
                    PlayerName = playerName,
                    Hash = hash,
                    IP = ip,
                });
            }

            return new PlayerAddMessage
            {
                Players = players.ToArray(),
            };
        }

        private IntelChangeMessage ReadIntelChangeMessage()
        {
            sbyte intel = _reader.ReadSByte();

            return new IntelChangeMessage
            {
                Intel = intel,
            };
        }

        private FlagListMessage ReadFlagListMessage(short messageLength)
        {
            var flags = new List<FlagListMessage.Flag>();
            while (_reader.BytesRead < messageLength)
            {
                var id = _reader.ReadInt16();
                var team = _reader.ReadByte();
                var position = _reader.ReadVector3();
                var radius = _reader.ReadUInt16();

                flags.Add(new FlagListMessage.Flag
                {
                    Id = id,
                    Team = team,
                    Position = position,
                    Radius = radius
                });
            }

            return new FlagListMessage { Flags = flags.ToArray() };
        }

        private ServerDetailsMessage ReadServerDetailsMessage()
        {
            var version = _reader.ReadInt32();
            var timePerTick = _reader.ReadSingle();
            var iP_Port = _reader.ReadCString();
            var serverName = _reader.ReadCString();
            var maxPlayers = _reader.ReadByte();
            var roundLength = _reader.ReadInt16();
            var briefingTime = _reader.ReadInt16();
            var mapName = _reader.ReadCString();
            var mapGamemode = _reader.ReadCString();
            var mapLayer = _reader.ReadByte();
            var opforTeamName = _reader.ReadCString();
            var bluforTeamName = _reader.ReadCString();
            var startTime = _reader.ReadInt32();
            var opforTickets = _reader.ReadUInt16();
            var bluforTickets = _reader.ReadUInt16();
            var mapSize = _reader.ReadSingle();

            return new ServerDetailsMessage()
            {
                Version = version,
                TimePerTick = timePerTick,
                IP_Port = iP_Port,
                ServerName = serverName,
                MaxPlayers = maxPlayers,
                RoundLength = roundLength,
                BriefingTime = briefingTime,
                MapName = mapName,
                MapGamemode = mapGamemode,
                MapLayer = mapLayer,
                OpforTeamName = opforTeamName,
                BluforTeamName = bluforTeamName,
                StartTime = startTime,
                OpforTickets = opforTickets,
                BluforTickets = bluforTickets,
                MapSize = mapSize,
            };
        }
    }
}