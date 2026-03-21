using SSMP.Math;
using SSMP.Networking.Packet;
using SSMPUtils.Utils;
using System;

namespace SSMPUtils.Client.Packets
{
    internal class TeleportPacket : IPacketData
    {
        public bool IsReliable => true;
        public bool DropReliableDataIfNewerExists => true;
        
        public ushort PlayerId;
        public string Scene = "";
        public Vector2 Position = Vector2.Zero;

        public void WriteData(IPacket packet)
        {
            packet.Write(PlayerId);
            packet.Write(Scene);
            packet.Write(Position);
        }

        public void ReadData(IPacket packet)
        {
            PlayerId = packet.ReadUShort();
            Scene = packet.ReadString();
            Position = packet.ReadVector2();
        }
    }

    internal class MessagePacket : IPacketData
    {
        public bool IsReliable => true;
        public bool DropReliableDataIfNewerExists => true;

        public ushort PlayerId;

        public Messages Message;

        public void WriteData(IPacket packet)
        {
            packet.Write(PlayerId);
            packet.Write((ushort)Message);
        }

        public void ReadData(IPacket packet)
        {
            PlayerId = packet.ReadUShort();
            Message = (Messages)packet.ReadUShort();
        }
    }

    internal class TeleportRequestPacket : IPacketData
    {
        public bool IsReliable => true;
        public bool DropReliableDataIfNewerExists => true;

        public ushort PlayerId;
        public void WriteData(IPacket packet)
        {
            packet.Write(PlayerId);
        }

        public void ReadData(IPacket packet)
        {
            PlayerId = packet.ReadUShort();
        }
    }

    internal class DeathPacket : IPacketData
    {
        public bool IsReliable => true;
        public bool DropReliableDataIfNewerExists => true;

        public ushort KillerID;
        public CauseOfDeath Cause = CauseOfDeath.Generic;
        public bool RanAway = false;
        public string Scene = "";
        public virtual void WriteData(IPacket packet)
        {
            packet.Write(KillerID);
            packet.Write((ushort)Cause);
            packet.Write(RanAway);
            packet.Write(Scene);
        }

        public virtual void ReadData(IPacket packet)
        {
            KillerID = packet.ReadUShort();
            Cause = (CauseOfDeath)packet.ReadUShort();
            RanAway = packet.ReadBool();
            Scene = packet.ReadString();
        }
    }

    public class HealthPacket : IPacketData
    {
        public bool IsReliable => true;
        public virtual bool DropReliableDataIfNewerExists => true;

        public ushort Masks;
        public ushort MaxHealth;
        public ushort BlueMasks;
        public bool LifebloodState;
        public virtual void WriteData(IPacket packet)
        {
            packet.Write(Masks);
            packet.Write(MaxHealth);
            packet.Write(BlueMasks);
            packet.Write(LifebloodState);
        }

        public virtual void ReadData(IPacket packet)
        {
            Masks = packet.ReadUShort();
            MaxHealth = packet.ReadUShort();
            BlueMasks = packet.ReadUShort();
            LifebloodState = packet.ReadBool();
        }
    }
    public static class Packets
    {
        internal static IPacketData Instantiate(PacketIDs packetID)
        {
            return packetID switch
            {
                PacketIDs.Huddle => new TeleportPacket(),
                PacketIDs.TeleportRequest => new TeleportRequestPacket(),
                PacketIDs.TeleportAccept => new TeleportPacket(),
                PacketIDs.Message => new MessagePacket(),
                PacketIDs.PlayerDeath => new DeathPacket(),
                PacketIDs.PlayerHealth => new HealthPacket(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
