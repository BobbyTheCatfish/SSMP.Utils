using SSMP.Api.Server;
using SSMP.Api.Server.Networking;
using SSMPUtils.Utils;
using SSMPUtils.Client.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSMPUtils.Server
{
    internal static class PacketReceiver
    {
        static IServerAddonNetworkReceiver<PacketIDs> receiver;

        public static void Init()
        {
            receiver = Server.api.NetServer.GetNetworkReceiver<PacketIDs>(Server.instance, Client.Packets.Packets.Instantiate);
            receiver.RegisterPacketHandler<HuddlePacket>(PacketIDs.Huddle, OnHuddle);
        }

        public static void OnHuddle(ushort id, HuddlePacket data)
        {
            PacketSender.BroadcastWarp(data.Scene, data.Position, id);
        }
    }
}
