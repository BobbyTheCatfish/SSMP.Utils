using SSMPUtils.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.GridLayoutGroup;

namespace SSMPUtils.Client.Modules
{
    internal class Warp
    {
        string scene;
        Vector2 position;

        public Warp(string scene, Vector2 position)
        {
            this.scene = scene;
            this.position = position;
        }

        void SetHornetPosition()
        {
            GameManager.instance.OnFinishedEnteringScene -= SetHornetPosition;
            Log.LogInfo("Setting hornet position");

            var hornet = Common.HornetObject;

            hornet.transform.SetPosition2D(position);
        }

        public void WarpToPosition()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            var hornet = Common.HornetObject;

            // Check if hornet even exists
            if (hornet == null)
            {
                FailedWarp(scene);
                return;
            }

            // No scene changes required
            if (scene == currentScene)
            {
                SetHornetPosition();
                return;
            }

            WarpToScene();
        }

        void WarpToScene()
        {
            // Get transition gate
            SceneTeleportMap.GetTeleportMap().TryGetValue(scene, out var teleport);
            if (teleport == null)
            {
                FailedWarp(scene);
                return;
            }

            GameManager.SceneLoadInfo loadInfo = new()
            {
                SceneName = scene,
                EntryGateName = teleport.TransitionGates[0],
                PreventCameraFadeOut = true,
                WaitForSceneTransitionCameraFade = false,
                AlwaysUnloadUnusedAssets = true,
                IsFirstLevelForPlayer = false
            };

            GameManager.instance.BeginSceneTransition(loadInfo);
            GameManager.instance.OnFinishedEnteringScene += SetHornetPosition;
        }
        

        static void FailedWarp(string scene)
        {
            Client.LocalChat($"I couldn't warp you. Please join the rest of the server in {scene}.");
        }
    }
}
