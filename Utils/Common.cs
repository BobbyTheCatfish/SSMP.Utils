using SSMP.Api.Client;
using SSMP.Api.Server;
using SSMP.Game;
using SSMP.Networking.Packet;
using System.Collections;
using UnityEngine;

namespace SSMPUtils.Utils
{
    internal class Common
    {
        public static GameObject HornetObject => HeroController.SilentInstance?.gameObject;

        public static IEnumerator SetHornetPosition(Vector2 location)
        {
            var hero = HeroController.instance;
            var game = GameManager.instance;
            var hornet = HornetObject;

            hornet.transform.SetPosition2D(location);
            hero.RelinquishControl();

            game.FinishedEnteringScene();
            hero.SetState(GlobalEnums.ActorStates.no_input);
            hero.ResetLook();
            hero.rb2d.isKinematic = false;
            hero.rb2d.linearVelocity = Vector2.zero;
            hero.HazardRespawnReset();
            yield return null;

            hero.SendHeroInPosition(forceDirect: false);

            yield return new WaitForSeconds(0.3f);
            GCManager.Collect();
            yield return null;

            hero.proxyFSM.SendEvent("HeroCtrl-HazardRespawned");
            hero.rb2d.interpolation = RigidbodyInterpolation2D.Interpolate;
            hero.FinishedEnteringScene(setHazardMarker: false);

            hero.RegainControl();
        }

        public static string GlobalTextColor(string text, Colors color)
        {
            var colorStr = color switch
            {
                Colors.White => "&f",
                Colors.Black => "&1",
                Colors.Orange => "&6",
                Colors.Purple => "&5",
                Colors.Blue => "&b",
                Colors.Green => "&2",
                Colors.Red => "&4",
                Colors.Yellow => "&e",
                _ => "&f"
            };

            return $"{colorStr}{text}&r";
        }

        public static string LocalTextColor(string text, Colors color)
        {
            var colorStr = color switch
            {
                Colors.White => "#FFFFFF",
                Colors.Black => "#000000",
                Colors.Orange => "#FFAA00",
                Colors.Purple => "#AA00AA",
                Colors.Blue => "#55FFFF",
                Colors.Green => "#00AA00",
                Colors.Red => "#AA0000",
                Colors.Yellow => "#FFFF55",
                _ => "#FFFFFF"
            };

            return $"<color={colorStr}>{text}</color>";
        }

        public static string ColoredUsername(IServerPlayer? player, Colors defaultColor = Colors.White)
        {
            if (player == null) return GlobalTextColor("Unknown Player", defaultColor);

            var username = player.Username;
            return player.Team switch
            {
                Team.Lifeblood => GlobalTextColor(username, Colors.Blue),
                Team.Moss => GlobalTextColor(username, Colors.Green),
                Team.Grimm => GlobalTextColor(username, Colors.Red),
                Team.Hive => GlobalTextColor(username, Colors.Orange),
                _ => GlobalTextColor(username, defaultColor),
            };
        }

        public static string ColoredUsername(IClientPlayer? player, Colors defaultColor = Colors.White)
        {
            if (player == null) return GlobalTextColor("Unknown Player", defaultColor);

            var username = player.Username;
            return player.Team switch
            {
                Team.Lifeblood => LocalTextColor(username, Colors.Blue),
                Team.Moss => LocalTextColor(username, Colors.Green),
                Team.Grimm => LocalTextColor(username, Colors.Red),
                Team.Hive => LocalTextColor(username, Colors.Orange),
                _ => LocalTextColor(username, defaultColor),
            };
        }

        static string ColoredUsername(string username, Team team, Colors defaultColor)
        {
            return team switch
            {
                Team.Lifeblood => GlobalTextColor(username, Colors.Blue),
                Team.Moss => GlobalTextColor(username, Colors.Green),
                Team.Grimm => GlobalTextColor(username, Colors.Red),
                Team.Hive => GlobalTextColor(username, Colors.Orange),
                _ => GlobalTextColor(username, defaultColor),
            };
        }
    }

    public class Packet : IPacketData
    {
        public virtual bool IsReliable => true;
        public virtual bool DropReliableDataIfNewerExists => true;

        public virtual void WriteData(IPacket packet)
        {
            Log.LogInfo("THIS SHOULD NOT RUN");
        }
        public virtual void ReadData(IPacket packet)
        {
            Log.LogInfo("THIS SHOULD NOT RUN");
        }
    }
}
