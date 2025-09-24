using EFT.Quests;
using SPT.Reflection.Patching;

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionLocationChecker : CheckerBasePatch
    {
        internal ConditionLocationChecker() : base(typeof(ConditionLocation))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionLocation ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            if (Globals.IgnoreLocationRequirements)
                __result = true;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionLocation ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionLocation condition = ___Condition;
            LogJsonDebug(condition);
        }
    }
}