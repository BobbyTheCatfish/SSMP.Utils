using SSMP.Networking.Packet;
using SSMPUtils.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SSMPUtils.Server.Packets
{
    internal class WarpPacket : Client.Packets.HuddlePacket { }

    public static class Packets
    {
        internal static IPacketData Instantiate(PacketIDs packetID)
        {
            switch (packetID)
            {
                case PacketIDs.Warp:
                    return new WarpPacket();
                default:
                    throw new NotImplementedException(packetID.ToString());
            }
        }
    }
}
