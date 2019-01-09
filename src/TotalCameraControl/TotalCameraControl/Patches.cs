using BattleTech;
using Harmony;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Reflection;

namespace TotalCameraControl
{

    /// <summary>
    /// Prevents general cinematics from showing.
    /// </summary>
    [HarmonyPatch(typeof(CameraControl))]
    [HarmonyPatch("setState")]
    class CameraControl_setState_Patch
    {
        public static bool Prefix(CameraControl __instance, CameraControl.CameraState newState)
        {
            if (!SequenceHelper.SkipSequenceByTrace(new StackTrace()))
            {
                if(TotalCameraControl.GlobalSettings.debug) Logger.Log("setState: Skipping!", false);
                return true;
            }
            else if (newState == CameraControl.CameraState.NotSet
                || newState == CameraControl.CameraState.RestoringPlayer
                || newState == CameraControl.CameraState.PlayerControlled)
            {
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// Deathcam has a special entry point
    /// </summary>
    [HarmonyPatch(typeof(AttackStackSequence))]
    [HarmonyPatch("OnActorDestroyed")]
    class AttackStackSequence_OnActorDestroyed_Patch
    {
        public static bool Prefix()
        {
            return !SequenceHelper.SkipSequenceByTrace(new StackTrace());
        }
    }

    /// <summary>
    /// Prevent randomized event's from triggering.
    /// </summary>
    [HarmonyPatch(typeof(CameraControl))]
    [HarmonyPatch("ShowRandomizedFocalCam")]
    [HarmonyPatch(new Type[] { typeof(Vector3), typeof(Quaternion), typeof(float), typeof(float), typeof(float) })]
    class CameraControl_ShowRandomizedFocalCam_Patch
    {
        public static bool Prefix()
        {
            return !SequenceHelper.SkipSequenceByTrace(new StackTrace());
        }
    }

    /// <summary>
    /// Cancel all remaining cinematics. Let death through as it may softlock the game when modded Mechs die.
    /// </summary>
    [HarmonyPatch(typeof(MultiSequence))]
    [HarmonyPatch("SetCamera")]
    class MultiSequence_SetCamera_Patch
    {

        public static bool Prefix(MultiSequence __instance, MethodBase __originalMethod, CameraSequence sequence, int messageIndex)
        {
            return !SequenceHelper.SkipSequenceByTrace(new StackTrace());
        }

    }

}