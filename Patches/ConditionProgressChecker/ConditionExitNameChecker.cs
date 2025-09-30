using EFT.Quests;
using SPT.Reflection.Patching;

#nullable enable

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionExitNameChecker : CheckerBasePatch
    {
        internal ConditionExitNameChecker() : base(typeof(ConditionExitName))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionExitName ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionExitName ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionUpdater.Update(ref ___Condition);
            LogJsonDebug(___Condition);
        }
    }
}