using EFT.Quests;
using SPT.Reflection.Patching;

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionQuestTimeChecker : CheckerBasePatch
    {
        internal ConditionQuestTimeChecker() : base(typeof(ConditionQuestTime))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionQuestTime ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionQuestTime ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionQuestTime condition = ___Condition;
            LogJsonDebug(condition);
        }
    }
}