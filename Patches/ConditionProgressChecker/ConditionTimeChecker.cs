using EFT.Quests;
using SPT.Reflection.Patching;

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionTimeChecker : CheckerBasePatch
    {
        internal ConditionTimeChecker() : base(typeof(ConditionTime))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionTime ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionTime ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionTime condition = ___Condition;
            LogJsonDebug(condition);
        }
    }
}