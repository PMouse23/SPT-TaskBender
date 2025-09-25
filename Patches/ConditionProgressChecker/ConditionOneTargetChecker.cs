using EFT.Quests;
using SPT.Reflection.Patching;

#nullable enable

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionOneTargetChecker : CheckerBasePatch
    {
        internal ConditionOneTargetChecker() : base(typeof(ConditionOneTarget))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionOneTarget ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionOneTarget ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionOneTarget condition = ___Condition;
            LogJsonDebug(condition);
        }
    }
}