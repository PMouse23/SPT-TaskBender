using EFT.Quests;
using SPT.Reflection.Patching;

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionCounterCreatorChecker : CheckerBasePatch
    {
        internal ConditionCounterCreatorChecker() : base(typeof(ConditionCounterCreator))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionCounterCreator ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionCounterCreator ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionCounterCreator condition = ___Condition;
            LogJsonDebug(condition);
        }
    }
}