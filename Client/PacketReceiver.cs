using SSMP.Api.Client.Networking;
using SSMPUtils.Utils;
using SSMPUtils.Server.Packets;
using SSMPUtils.Client.Modules;

namespace SSMPUtils.Client
{
    internal static class PacketReceiver
    {
        static IClientAddonNetworkReceiver<PacketIDs> receiver;

        public static void Init()
        {
            receiver = Client.api.NetClient.GetNetworkReceiver<PacketIDs>(Client.instance, Server.Packets.Packets.Instantiate);
            receiver.RegisterPacketHandler<WarpPacket>(PacketIDs.Warp, OnHuddle);
        }

        public static void OnHuddle(WarpPacket data)
        {
            var huddleWarp = new Warp(data.Scene, data.Position);
            huddleWarp.WarpToPosition();
        }

        public static void OnTeleportAccepted(WarpPacket data)
        {
            var teleportWarp = new Warp(data.Scene, data.Position);
            teleportWarp.WarpToPosition();
        }
    }
}
