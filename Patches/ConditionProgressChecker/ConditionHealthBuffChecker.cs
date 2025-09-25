using EFT.Quests;
using SPT.Reflection.Patching;

#nullable enable

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionHealthBuffChecker : CheckerBasePatch
    {
        internal ConditionHealthBuffChecker() : base(typeof(ConditionHealthBuff))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionHealthBuff ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            if (Globals.IgnoreHealthBuffRequirements)
                __result = true;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionHealthBuff ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionHealthBuff condition = ___Condition;
            LogJsonDebug(condition);
        }
    }
}