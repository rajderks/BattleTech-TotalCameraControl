using System;
using System.Reflection;
using BattleTech;
using System.Diagnostics;

namespace TotalCameraControl
{
   
    static class SequenceHelper
    {
        public static readonly Type Jump = typeof(MechJumpSequence);
        public static readonly Type DFA = typeof(MechDFASequence);
        public static readonly Type Melee = typeof(MechMeleeSequence);
        public static readonly Type AttackStack = typeof(AttackStackSequence);
        public static readonly Type ArtilleryObjective = typeof(ArtilleryObjectiveSequence);
        public static readonly Type Artillery = typeof(ArtillerySequence);
        public static readonly Type Move = typeof(ActorMovementSequence);
        public static readonly Type Startup = typeof(MechStartupSequence);
        public static readonly Type Stand = typeof(MechStandSequence);
        public static readonly Type Multi = typeof(MultiSequence);
        public static readonly Type SensorLock = typeof(SensorLockSequence);
        public static readonly Type ShowActor = typeof(ShowActorInfoSequence);
        public static readonly Type Strafe = typeof(StrafeSequence);
        public static readonly Type ActorDestroyed = typeof(AttackStackSequence);
        
        public static bool SkipSequenceByTrace(StackTrace stackTrace)
        {
            Settings settings = TotalCameraControl.GlobalSettings;
            for (var i = 0; i < stackTrace.FrameCount; i++)
            {
                MethodBase mbase = stackTrace.GetFrame(i).GetMethod();
                try
                {
                    Type declaringType = mbase.DeclaringType;
                    if (declaringType == Jump && settings.jump) return true;
                    if (declaringType == DFA && settings.DFA) return true;
                    if (declaringType == Melee && settings.melee) return true;
                    if (declaringType == ArtilleryObjective && settings.artilleryObjective) return true;
                    if (declaringType == Artillery && settings.artillery) return true;
                    if (declaringType == Move && settings.move) return true;
                    if (declaringType == Startup && settings.startup) return true;
                    if (declaringType == Stand && settings.stand) return true;
                    if (declaringType == SensorLock && settings.sensorLock) return true;
                    if (declaringType == ShowActor && settings.showActor) return true;
                    if (declaringType == Strafe && settings.strafe) return true;
                    if (declaringType == ActorDestroyed && settings.deathCam) return true;
                    if (TotalCameraControl.GlobalSettings.debug) Logger.Log(new StackTrace().ToString(), true);
                }
                catch (Exception e)
                {
                    Logger.Log(mbase.ToString(), false);
                    Logger.Error(e);
                }
            }
            return false;
        }

    }

}
/*

-----------------------------------------------------------------------------

   at TotalCameraControl.SequenceHelper.SkipSequenceByTrace(System.Diagnostics.StackTrace stackTrace)
   at TotalCameraControl.MultiSequence_SetCamera_Patch.Prefix(BattleTech.MultiSequence __instance, System.Reflection.MethodBase __originalMethod, BattleTech.CameraSequence sequence, Int32 messageIndex)
   at BattleTech.MultiSequence.SetCamera_Patch1(System.Object , BattleTech.CameraSequence , Int32 )
   at BattleTech.AttackStackSequence.OnActorDestroyed(.MessageCenterMessage message)
   at MessageCenter.SendMessagesForType(MessageCenterMessageType messageType, .MessageCenterMessage message)
   at MessageCenter.PublishMessage(.MessageCenterMessage message)
   at BattleTech.AbstractActor.HandleDeath(System.String attackerGUID)
   at BattleTech.Mech.HandleDeath(System.String attackerGUID)
   at BattleTech.AttackDirector+AttackSequence.OnAttackSequenceImpact_Patch2(System.Object, .MessageCenterMessage )
   at MessageCenter.SendMessagesForType(MessageCenterMessageType messageType, .MessageCenterMessage message)
   at MessageCenter.PublishMessage(.MessageCenterMessage message)
   at WeaponEffect.OnImpact(Single hitDamage)
   at PPCEffect.OnImpact(Single hitDamage)
   at WeaponEffect.PlayImpact()
   at PPCEffect.PlayImpact()
   at PPCEffect.Update()
Date :1/9/2019 7:55:10 PM

*/
