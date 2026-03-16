using SSMP.Api.Client;
using SSMP.Api.Client.Networking;
using SSMPUtils.Client.Packets;
using SSMPUtils.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SSMPUtils.Client
{
    internal static class PacketSender
    {
        static IClientAddonNetworkSender<PacketIDs> sender;
        internal static void Init()
        {
            sender = Client.api.NetClient.GetNetworkSender<PacketIDs>(Client.instance);
        }
        internal static void SendHuddle()
        {
            if (GameManager.SilentInstance == null || GameManager.SilentInstance.GameState != GlobalEnums.GameState.PLAYING)
            {
                Client.LocalChat("Resume the game first!");
                return;
            }

            var scene = SceneManager.GetActiveScene().name;
            var gate = GameManager.instance.entryGateName;

            if (scene == null)
            {
                Client.LocalChat("I couldn't find the current scene!");
                Log.LogError("ACTUAL ERROR: Unable to find current scene.");
                return;
            }

            var hornet = Common.HornetObject;
            if (hornet == null)
            {
                Client.LocalChat("I couldn't find your position. Uh oh!");
                Log.LogError("ACTUAL ERROR: Unable to find hornet object");
            }

            var location = (Vector2)hornet.transform.position;

            Log.LogDebug($"Sending huddle packet for scene {scene} at location {location}");
            sender.SendSingleData(PacketIDs.Huddle, new HuddlePacket
            {
                scene = scene,
                location = location,
            });
        }
    }
}
