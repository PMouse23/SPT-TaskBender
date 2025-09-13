using EFT.Quests;
using SPT.Reflection.Patching;

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionPrestigeLevelChecker : CheckerBasePatch
    {
        internal ConditionPrestigeLevelChecker() : base(typeof(ConditionPrestigeLevel))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionPrestigeLevel ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionPrestigeLevel ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionPrestigeLevel condition = ___Condition;
            LogJsonDebug(condition);
        }
    }
}