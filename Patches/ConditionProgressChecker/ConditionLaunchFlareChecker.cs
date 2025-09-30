using EFT.Quests;
using SPT.Reflection.Patching;

#nullable enable

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionLaunchFlareChecker : CheckerBasePatch
    {
        internal ConditionLaunchFlareChecker() : base(typeof(ConditionLaunchFlare))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionLaunchFlare ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionLaunchFlare ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionUpdater.Update(ref ___Condition);
            LogJsonDebug(___Condition);
        }
    }
}