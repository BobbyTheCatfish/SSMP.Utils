using SSMP.Networking.Packet;
using SSMPUtils.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SSMPUtils.Client.Packets
{
    internal class HuddlePacket : IPacketData
    {
        public bool IsReliable => true;
        public bool DropReliableDataIfNewerExists => true;

        public string Scene = "";
        public Vector2 Position;

        public void WriteData(IPacket packet)
        {
            packet.Write(Scene);
            packet.Write(Position.x);
            packet.Write(Position.y);
        }

        public void ReadData(IPacket packet)
        {
            Scene = packet.ReadString();
            Position = new Vector2(packet.ReadFloat(), packet.ReadFloat());
        }
    }

    public static class Packets
    {
        internal static IPacketData Instantiate(PacketIDs packetID)
        {
            switch (packetID)
            {
                case PacketIDs.Huddle:
                    return new HuddlePacket();
                default:
                    throw new NotImplementedException(packetID.ToString());
            }
        }
    }
}
