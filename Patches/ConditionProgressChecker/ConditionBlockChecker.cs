using EFT.Quests;
using SPT.Reflection.Patching;

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionBlockChecker : CheckerBasePatch
    {
        internal ConditionBlockChecker() : base(typeof(ConditionBlock))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionBlock ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionBlock ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionBlock condition = ___Condition;
            LogJsonDebug(condition);
        }
    }
}