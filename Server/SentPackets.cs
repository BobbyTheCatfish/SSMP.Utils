using SSMP.Networking.Packet;
using SSMPUtils.Client.Packets;
using SSMPUtils.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SSMPUtils.Server.Packets
{
    public static class Packets
    {
        internal class PlayerHealthPacket : HealthPacket
        {
            public ushort PlayerId;

            public override bool DropReliableDataIfNewerExists => false;

            public override void WriteData(IPacket packet)
            {
                packet.Write(PlayerId);
                base.WriteData(packet);
            }

            public override void ReadData(IPacket packet)
            {
                PlayerId = packet.ReadUShort();
                base.ReadData(packet);
            }
        }

        internal static IPacketData Instantiate(PacketIDs packetID)
        {
            return packetID switch
            {
                PacketIDs.Huddle => new TeleportPacket(),
                PacketIDs.TeleportRequest => new TeleportRequestPacket(),
                PacketIDs.TeleportAccept => new TeleportPacket(),
                PacketIDs.Message => new MessagePacket(),
                PacketIDs.PlayerHealth => new PlayerHealthPacket(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
