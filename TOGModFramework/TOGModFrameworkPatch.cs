using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace TOGModFramework
{
    public class TOGModFrameworkPatch : MonoBehaviour
    {
        private Harmony _harmony;

        public void Inject()
        {
            try
            {
                _harmony = new Harmony("ModFramework");
                _harmony.PatchAll(Assembly.GetExecutingAssembly());
                Debug.Log("ModFramework Loaded");
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        [HarmonyPatch(typeof(VRCharController))]
        [HarmonyPatch("EnableAvatar")]
        // ReSharper disable once UnusedType.Local
        private static class VRCharControllerEnableAvatarPatch
        {
            [HarmonyPostfix]
            private static void Postfix(VRCharController __instance)
            {
                Player.local = __instance;
                ConfigManager.local = __instance.config;
                EventManager.InvokePlayerLoadedEvent();
            }
        }
        
        [HarmonyPatch(typeof(CrossBowScript))]
        [HarmonyPatch("FireBolt")]
        // ReSharper disable once UnusedType.Local
        private static class CrossBowScriptFireBoltPatch
        {
            [HarmonyPostfix]
            private static void Postfix(CrossBowScript __instance)
            {
                EventManager.InvokeCrossbowBoltFiredEvent(__instance);
            }
        }
    }
}