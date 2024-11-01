using MelonLoader;
using HarmonyLib;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using MyTrueGear;

namespace COMPOUND_TrueGear
{
    public static class BuildInfo
    {
        public const string Name = "COMPOUND_TrueGear"; // Name of the Mod.  (MUST BE SET)
        public const string Description = "TrueGear Mod for COMPOUND"; // Description for the Mod.  (Set as null if none)
        public const string Author = "HuangLY"; // Author of the Mod.  (MUST BE SET)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class COMPOUND_TrueGear : MelonMod
    {
        private enum GunTyp{ 
            Pistol,
            Rifle,
            Shotgun
        };
        private static int lastHealth = 0;
        private static bool canHurt = false;
        private static TrueGearMod _TrueGear = null;
        private static bool freeReloadEff = false;
        private static bool helmetEff = false;
        private static bool mapClearEff = false;


        public override void OnInitializeMelon() {
            MelonLogger.Msg("OnApplicationStart");
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(COMPOUND_TrueGear));
            _TrueGear = new TrueGearMod();
        }

        public static KeyValuePair<float, float> GetAngle(Transform player, Vector3 hit)
        {
            // 计算玩家和击中点之间的向量
            Vector3 direction = hit - player.position;

            // 计算玩家正前方向量在水平面上的投影
            Vector3 forward = Vector3.ProjectOnPlane(player.forward, Vector3.up).normalized;

            // 计算夹角
            float angle = Vector3.SignedAngle(forward, direction, Vector3.up);

            angle = 360f - ((angle + 360f) % 360f);

            // 计算垂直偏移量
            float verticalOffset = player.transform.position.y - hit.y;

            return new KeyValuePair<float, float>(angle, verticalOffset);
        }


        [HarmonyPostfix, HarmonyPatch(typeof(GunController), "FireSilent")]
        private static void GunController_FireSilent_Postfix(GunController __instance)
        {
            GunTyp gunTyp = GunTyp.Pistol;
            try {
                gunTyp = GetGunTyp(__instance.WeaponID.ToString());
            }
            catch (Exception e)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("GunControllerFireError :" + e);
                gunTyp = GunTyp.Shotgun;
            }
            switch (gunTyp)
            {
                case GunTyp.Pistol:
                    if (__instance.VRWrapper != null && __instance.SecondaryVRWrapper != null)
                    {
                        MelonLogger.Msg("-----------------------------");
                        MelonLogger.Msg("LeftHandPistolShoot");
                        MelonLogger.Msg("RightHandPistolShoot");
                        _TrueGear.Play("LeftHandPistolShoot");
                        _TrueGear.Play("RightHandPistolShoot");
                    }
                    else if (__instance.IsLeftHandedGun)
                    {
                        MelonLogger.Msg("-----------------------------");
                        MelonLogger.Msg("LeftHandPistolShoot");
                        _TrueGear.Play("LeftHandPistolShoot");
                    }
                    else
                    {
                        MelonLogger.Msg("-----------------------------");
                        MelonLogger.Msg("RightHandPistolShoot");
                        _TrueGear.Play("RightHandPistolShoot");
                    }
                    break;
                case GunTyp.Rifle:
                    if (__instance.VRWrapper != null && __instance.SecondaryVRWrapper != null)
                    {
                        MelonLogger.Msg("-----------------------------");
                        MelonLogger.Msg("LeftHandRifleShoot");
                        MelonLogger.Msg("RightHandRifleShoot");
                        _TrueGear.Play("LeftHandRifleShoot");
                        _TrueGear.Play("RightHandRifleShoot");
                    }
                    else if (__instance.IsLeftHandedGun)
                    {
                        MelonLogger.Msg("-----------------------------");
                        MelonLogger.Msg("LeftHandRifleShoot");
                        _TrueGear.Play("LeftHandRifleShoot");
                    }
                    else
                    {
                        MelonLogger.Msg("-----------------------------");
                        MelonLogger.Msg("RightHandRifleShoot");
                        _TrueGear.Play("RightHandRifleShoot");
                    }
                    break;
                case GunTyp.Shotgun:
                    if (__instance.VRWrapper != null && __instance.SecondaryVRWrapper != null)
                    {
                        MelonLogger.Msg("-----------------------------");
                        MelonLogger.Msg("LeftHandShotgunShoot");
                        MelonLogger.Msg("RightHandShotgunShoot");
                        _TrueGear.Play("LeftHandShotgunShoot");
                        _TrueGear.Play("RightHandShotgunShoot");
                    }
                    else if (__instance.IsLeftHandedGun)
                    {
                        MelonLogger.Msg("-----------------------------");
                        MelonLogger.Msg("LeftHandShotgunShoot");
                        _TrueGear.Play("LeftHandShotgunShoot");
                    }
                    else
                    {
                        MelonLogger.Msg("-----------------------------");
                        MelonLogger.Msg("RightHandShotgunShoot");
                        _TrueGear.Play("RightHandShotgunShoot");
                    }
                    break;
                default:
                    if (__instance.VRWrapper != null && __instance.SecondaryVRWrapper != null)
                    {
                        MelonLogger.Msg("-----------------------------");
                        MelonLogger.Msg("LeftHandPistolShoot");
                        MelonLogger.Msg("RightHandPistolShoot");
                        _TrueGear.Play("LeftHandPistolShoot");
                        _TrueGear.Play("RightHandPistolShoot");
                    }
                    else if (__instance.IsLeftHandedGun)
                    {
                        MelonLogger.Msg("-----------------------------");
                        MelonLogger.Msg("LeftHandPistolShoot");
                        _TrueGear.Play("LeftHandPistolShoot");
                    }
                    else
                    {
                        MelonLogger.Msg("-----------------------------");
                        MelonLogger.Msg("RightHandPistolShoot");
                        _TrueGear.Play("RightHandPistolShoot");
                    }
                    break;
            }
            
            MelonLogger.Msg(__instance.IsLeftHandedGun);
            MelonLogger.Msg(__instance.WeaponID);
            MelonLogger.Msg(__instance.HeldHand);
        }

        private static GunTyp GetGunTyp(string weaponID) 
        {
            if (weaponID.Contains("SMG"))
            {
                return GunTyp.Rifle;
            }
            else if (weaponID.Contains("Grenade") || weaponID.Contains("DoubleShotgun"))
            {
                return GunTyp.Shotgun;
            }
            else
            {
                return GunTyp.Pistol;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayerController), "OnHurt")]
        private static void PlayerController_OnHurt_Postfix(PlayerController __instance)
        {
            MelonLogger.Msg("-----------------------------");
            MelonLogger.Msg("OnHurt");
            canHurt = true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayerController), "Update")]
        private static void PlayerController_Update_Postfix(PlayerController __instance)
        {
            if (lastHealth != __instance.DamageableComponent.GetCurrentHealth())
            {
                lastHealth = __instance.DamageableComponent.GetCurrentHealth();
                if (__instance.DamageableComponent.GetCurrentHealth() == 1)
                {
                    MelonLogger.Msg("-----------------------------");
                    MelonLogger.Msg("StartHeartBeat");
                    _TrueGear.StartHeartBeat();
                }
                else
                {
                    MelonLogger.Msg("-----------------------------");
                    MelonLogger.Msg("StopHeartBeat");
                    _TrueGear.StopHeartBeat();
                }
                MelonLogger.Msg(__instance.DamageableComponent.GetCurrentHealth());
            }            
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayerController), "OnDeath")]
        private static void PlayerController_OnDeath_Postfix(PlayerController __instance)
        {
            MelonLogger.Msg("-----------------------------");
            MelonLogger.Msg("PlayerDeath");
            _TrueGear.Play("PlayerDeath");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Food), "OnEat")]
        private static void Food_OnEat_Postfix(Food __instance)
        {
            MelonLogger.Msg("-----------------------------");
            MelonLogger.Msg("Eating");
            _TrueGear.Play("Eating");
        }

        //[HarmonyPostfix, HarmonyPatch(typeof(MutatorDisabler), "OnTriggerStay")]
        //private static void MutatorDisabler_OnTriggerStay_Postfix(MutatorDisabler __instance)
        //{
        //    MelonLogger.Msg("-----------------------------");
        //    MelonLogger.Msg("Shower");
        //}

        [HarmonyPostfix, HarmonyPatch(typeof(SyringeController), "Inject")]
        private static void SyringeController_Inject_Postfix(SyringeController __instance)
        {
            MelonLogger.Msg("-----------------------------");
            MelonLogger.Msg("Injection");
            _TrueGear.Play("Injection");
        }

        //[HarmonyPrefix, HarmonyPatch(typeof(EmergencyFirearm), "FixedUpdate")]
        //private static void EmergencyFirearm_FixedUpdate_Prefix(EmergencyFirearm __instance)
        //{
        //    if (__instance.Firearm == null || __instance.FirearmPrefab == null || __instance.CaseGlass == null || __instance.LeftHand == null)
        //    {
        //        return;
        //    }
        //    if (__instance.LeftHand != null)
        //    {
        //        MelonLogger.Msg("-----------------------------");
        //        MelonLogger.Msg("LeftHandEmergencyFirearm");
        //    }
        //    if (__instance.RightHand != null)
        //    {
        //        MelonLogger.Msg("-----------------------------");
        //        MelonLogger.Msg("RightHandEmergencyFirearm");
        //    }
        //}

        [HarmonyPostfix, HarmonyPatch(typeof(Explosion), "CheckIfVisible")]
        private static void Explosion_CheckIfVisible_Postfix(Explosion __instance, bool __result, Vector3 checkPosition)
        {
            if (__result && Vector3.Distance(checkPosition, __instance.transform.position) < 5f)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("Explosion");
                _TrueGear.Play("Explosion");
            }
        }


        [HarmonyPostfix, HarmonyPatch(typeof(InventoryManager), "MoveToInventory",new Type[] { typeof(Loadable),typeof(bool) })]
        private static void InventoryManager_MoveToInventory_Postfix(InventoryManager __instance, Loadable ammo)
        {
            if (!ammo.OtherHandVRWrapper.IsLeftHand)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("ChestSlotInputItemLeft");
                _TrueGear.Play("ChestSlotInputItemLeft");
            }
            else 
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("ChestSlotInputItemRight");
                _TrueGear.Play("ChestSlotInputItemRight");
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(InventoryManager), "TakeAmmoFromInventory", new Type[] { typeof(Loadable) })]
        private static void InventoryManager_TakeAmmoFromInventory_Postfix(InventoryManager __instance, Loadable loadable)
        {
            if (!loadable.OtherHandVRWrapper.IsLeftHand)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("ChestSlotOutputItemLeft");
                _TrueGear.Play("ChestSlotOutputItemLeft");
            }
            else
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("ChestSlotOutputItemRight");
                _TrueGear.Play("ChestSlotOutputItemRight");
            }
            
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Grabber), "Grab", new Type[] { typeof(Grabber.GrabbableObject) })]
        private static void Grabber_Grab_Postfix(Grabber __instance, Grabber.GrabbableObject objectToGrab)
        {
            if (__instance.VRWrapper.IsLeftHand)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("LeftHandPickupItem");
                _TrueGear.Play("LeftHandPickupItem");
            }
            else
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("RightHandPickupItem");
                _TrueGear.Play("RightHandPickupItem");
            }
            MelonLogger.Msg(__instance.VRWrapper.IsLeftHand);
            MelonLogger.Msg(objectToGrab.ToString());
            MelonLogger.Msg(objectToGrab.GrabbedObject.name);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Grabber), "UnGrab", new Type[] { typeof(bool) })]
        private static void Grabber_UnGrab_Postfix(Grabber __instance, bool silent)
        {
            MelonLogger.Msg("-----------------------------");
            MelonLogger.Msg("UnGrab");
            MelonLogger.Msg(silent);
            MelonLogger.Msg(__instance.VRWrapper.IsLeftHand);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(EmergencyFirearm), "FixedUpdate")]
        private static void EmergencyFirearm_FixedUpdate_Prefix(EmergencyFirearm __instance)
        {
            if (__instance.Firearm == null || __instance.FirearmPrefab == null)
            {
                return;
            }
            if (__instance.CaseGlass != null && __instance.LeftHand != null && (__instance.CaseGlass.transform.position - __instance.LeftHand.transform.position).sqrMagnitude < 0.0225f && (__instance.CaseGlass.transform.forward * Vector3.Dot(__instance.LeftHand.GetSmoothedLinearVelocity(), __instance.CaseGlass.transform.forward)).sqrMagnitude > 1f)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("LeftHandBreakGlass");
                _TrueGear.Play("LeftHandBreakGlass");
            }
            if (__instance.CaseGlass != null && __instance.RightHand != null && (__instance.CaseGlass.transform.position - __instance.RightHand.transform.position).sqrMagnitude < 0.0225f && (__instance.CaseGlass.transform.forward * Vector3.Dot(__instance.RightHand.GetSmoothedLinearVelocity(), __instance.CaseGlass.transform.forward)).sqrMagnitude > 1f)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("RightHandBreakGlass");
                _TrueGear.Play("RightHandBreakGlass");
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Damageable), "TakeDamage")]
        private static void Damageable_TakeDamage_Postfix(Damageable __instance, int damage, GameObject damageDealer)
        {
            if (__instance.tag != "Player")
            {
                return;
            }

            if (!canHurt)
            {
                return;
            }
            if (__instance.CurrentHealth <= 0 || damage <= 0)
            {
                return;
            }
            MelonLogger.Msg("-----------------------------");
            MelonLogger.Msg("TakeDamage");
            canHurt = false;
            if (damageDealer == null)
            {
                MelonLogger.Msg("PoisonDamage");
                _TrueGear.Play("PoisonDamage");
                return;
            }

            var angle = GetAngle(__instance.transform.parent, damageDealer.transform.position);            
            MelonLogger.Msg($"DefaultDamage,{angle.Key},{angle.Value}");
            _TrueGear.PlayAngle("DefaultDamage", angle.Key, 0);
            MelonLogger.Msg(damageDealer.name);
            MelonLogger.Msg(__instance.name);
            MelonLogger.Msg(__instance.tag);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(TeleportMarker), "Teleport")]
        private static void TeleportMarker_Teleport_Postfix(TeleportMarker __instance)
        {
            MelonLogger.Msg("-----------------------------");
            MelonLogger.Msg("Teleport");
            _TrueGear.Play("Teleport");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(StickyLauncher), "Fire")]
        private static void StickyLauncher_Fire_Postfix(StickyLauncher __instance)
        {
            if (__instance.VRWrapper != null && __instance.SecondaryVRWrapper != null)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("LeftHandShotgunShoot");
                MelonLogger.Msg("RightHandShotgunShoot");
                _TrueGear.Play("LeftHandShotgunShoot");
                _TrueGear.Play("RightHandShotgunShoot");
            }
            else if (__instance.IsLeftHandedGun)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("LeftHandShotgunShoot");
                _TrueGear.Play("LeftHandShotgunShoot");
            }
            else
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("RightHandShotgunShoot");
                _TrueGear.Play("RightHandShotgunShoot");
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(CompoundBow), "OnStringRelease")]
        private static void CompoundBow_OnStringRelease_Prefix(CompoundBow __instance)
        {
            if (!__instance.GrabComponent.IsGrabbed)
            {
                return;
            }
            float num = __instance.DrawOffset * 1.33334f;
            if (num <= 0.5f)
            {
                return;
            }
            if (!__instance.IsLeftHandedGun)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("LeftHandStringRelease");
                _TrueGear.Play("LeftHandStringRelease");
            }
            else
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("RightHandStringRelease");
                _TrueGear.Play("RightHandStringRelease");
            }            
        }


        [HarmonyPostfix, HarmonyPatch(typeof(VisiblePickup), "Update")]
        private static void VisiblePickup_Update_Postfix(VisiblePickup __instance)
        {
            if (Singleton<GlobalGameInfo>.Instance.GameControllerInstance.FreeReloadPowerupEffect.active && !freeReloadEff)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("FreeReloads");
                _TrueGear.Play("FreeReloads");
                freeReloadEff = true;
            }
            else if (!Singleton<GlobalGameInfo>.Instance.GameControllerInstance.FreeReloadPowerupEffect.active)
            {
                freeReloadEff = false;
            }
            if (Singleton<GlobalGameInfo>.Instance.GameControllerInstance.ShieldPowerupEffect.active && !helmetEff)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("Helmet");
                _TrueGear.Play("Helmet");
                helmetEff = true;
            }
            else if (!Singleton<GlobalGameInfo>.Instance.GameControllerInstance.ShieldPowerupEffect.active && helmetEff)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("BreakHelmet");
                _TrueGear.Play("BreakHelmet");
                helmetEff = false;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(GameController), "Update")]
        private static void GameController_Update_Postfix(GameController __instance)
        {
            if (__instance.MapClearScreenShown && !mapClearEff)
            {
                MelonLogger.Msg("-----------------------------");
                MelonLogger.Msg("MapsCleared");
                _TrueGear.Play("MapsCleared");
                mapClearEff = true;
            }
            else if (!__instance.MapClearScreenShown)
            {
                mapClearEff = false;
            }
        }

    }
}