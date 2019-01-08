using BattleTech;
using Harmony;
using UnityEngine;

namespace TotalCameraControl
{
    /// <summary>
    /// Prevents general cinematics from showing.
    /// </summary>
    [HarmonyPatch(typeof(CameraControl))]
    [HarmonyPatch("setState")]
    class CameraControl_setState_Patch
    {
        public static bool Prefix(CameraControl __instance, CameraControl.CameraState newState) {

            if(newState == CameraControl.CameraState.NotSet 
                || newState == CameraControl.CameraState.RestoringPlayer
                || newState == CameraControl.CameraState.PlayerControlled)
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Prevent randomized event's from triggering, i.e. melee.
    /// </summary>
    [HarmonyPatch(typeof(CameraControl))]
    [HarmonyPatch("ShowRandomizedFocalCam")]
    [HarmonyPatch(new System.Type[] { typeof(Vector3), typeof(Quaternion), typeof(float), typeof(float), typeof(float) })]
    class CameraControl_ShowRandomizedFocalCam_Patch
    {
        public static bool Prefix()
        {
            return false;
        }
    }

    /// <summary>
    /// Prevents enemy movement, standup and startup animations.
    /// </summary>
    [HarmonyPatch(typeof(ActorMovementSequence))]
    [HarmonyPatch("ShowCamera")]
    class ActorMovementSequence_ShowCamera_Patch
    {
        public static bool Prefix()
        {
            return false;
        }
    }

    /// <summary>
    /// Cancel all remaining cinematics. Let death through as it may softlock the game when modded Mechs die.
    /// </summary>
    [HarmonyPatch(typeof(MultiSequence))]
    [HarmonyPatch("SetCamera")]
    class MultiSequence_SetCamera_Patch
    {
        public static bool Prefix(MultiSequence __instance, CameraSequence sequence)
        {
            try
            {
                if (sequence.FocalCombatant.IsDead)
                {
                    return true;
                }
                return false;
            } catch(System.Exception e)
            {
                return true;
            }
        }
    }
}

