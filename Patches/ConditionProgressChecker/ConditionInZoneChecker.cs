using EFT.Quests;
using SPT.Reflection.Patching;

#nullable enable

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionInZoneChecker : CheckerBasePatch
    {
        internal ConditionInZoneChecker() : base(typeof(ConditionInZone))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionInZone ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            if (Globals.IgnoreInZoneRequirements)
                __result = true;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionInZone ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionUpdater.Update(ref ___Condition);
            LogJsonDebug(___Condition);
        }
    }
}