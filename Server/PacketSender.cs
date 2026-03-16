using SSMP.Api.Server;
using SSMP.Api.Server.Networking;
using SSMP.Networking.Packet;
using SSMPUtils.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SSMPUtils.Server
{
    internal static class PacketSender
    {
        static IServerAddonNetworkSender<PacketIDs> sender;
        internal static void Init()
        {
            sender = Server.api.NetServer.GetNetworkSender<PacketIDs>(Server.instance);
        }

        static void Broadcast(PacketIDs id, IPacketData packet, ushort senderId)
        {
            var players = Server.api.ServerManager.Players;
            foreach (var player in players)
            {
                sender.SendSingleData(id, packet, player.Id);
            }
        }

        internal static void BroadcastWarp(string scene, Vector2 location, ushort senderId)
        {
            Broadcast(PacketIDs.Warp, new Client.Packets.HuddlePacket
            {
                Scene = scene,
                Position = location
            }, senderId);
        }
    }
}
