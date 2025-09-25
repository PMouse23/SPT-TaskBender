using EFT.Quests;
using SPT.Reflection.Patching;

#nullable enable

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionMultipleTargetsChecker : CheckerBasePatch
    {
        internal ConditionMultipleTargetsChecker() : base(typeof(ConditionMultipleTargets))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionMultipleTargets ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionMultipleTargets ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionMultipleTargets condition = ___Condition;
            LogJsonDebug(condition);
        }
    }
}