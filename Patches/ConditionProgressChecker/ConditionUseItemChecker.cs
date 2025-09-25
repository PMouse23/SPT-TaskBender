using EFT.Quests;
using SPT.Reflection.Patching;

#nullable enable

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionUseItemChecker : CheckerBasePatch
    {
        internal ConditionUseItemChecker() : base(typeof(ConditionUseItem))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionUseItem ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionUseItem ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionUseItem condition = ___Condition;
            LogJsonDebug(condition);
        }
    }
}