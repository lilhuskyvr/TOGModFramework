using System;
using System.Collections;
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
            private static IEnumerator LoadWeapons()
            {
                yield return new WaitForSeconds(2);

                ModFrameworkWeaponSpawner.local = new ModFrameworkWeaponSpawner();

                foreach (var weaponRack in FindObjectsOfType<WeaponRack>())
                {
                    foreach (var slot in weaponRack.Slots)
                    {
                        ModFrameworkWeaponSpawner.local.weapons.Add(slot);
                    }
                }

                ModFrameworkWeaponSpawner.local.loaded = true;
                ModFrameworkWeaponSpawner.InvokeWeaponsLoadedEvent();

                yield return null;
            }

            [HarmonyPostfix]
            private static void Postfix(VRCharController __instance)
            {
                Player.local = __instance;
                ConfigManager.local = __instance.config;

                ConfigManager.local.StartCoroutine(LoadWeapons());

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

        [HarmonyPatch(typeof(LeftHandGrab))]
        [HarmonyPatch("CreateJoint")]
        // ReSharper disable once UnusedType.Local
        private static class LeftHandGrabCreateJointPatch
        {
            [HarmonyPostfix]
            private static void Postfix(LeftHandGrab __instance)
            {
                EventManager.InvokeWeaponJointCreatedEvent(__instance.wp, __instance.wp.f);
            }
        }

        [HarmonyPatch(typeof(RightHandGrab))]
        [HarmonyPatch("CreateJoint")]
        // ReSharper disable once UnusedType.Local
        private static class RightHandGrabCreateJointPatch
        {
            [HarmonyPostfix]
            private static void Postfix(RightHandGrab __instance)
            {
                EventManager.InvokeWeaponJointCreatedEvent(__instance.wp, __instance.wp.f);
            }
        }

        [HarmonyPatch(typeof(WeaponBlock))]
        [HarmonyPatch("OnCollisionEnter")]
        // ReSharper disable once UnusedType.Local
        private static class WeaponBlockOnCollisionEnterPatch
        {
            [HarmonyPostfix]
            private static void Postfix(WeaponBlock __instance, Collision collision)
            {
                EventManager.InvokeWeaponOnCollisionEnteredEvent(__instance.wp, collision, __instance.stats,
                    collision.collider.GetComponentInParent<Stats>());
            }
        }
    }
}