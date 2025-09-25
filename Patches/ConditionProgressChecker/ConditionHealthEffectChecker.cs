using EFT.Quests;
using SPT.Reflection.Patching;

#nullable enable

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionHealthEffectChecker : CheckerBasePatch
    {
        internal ConditionHealthEffectChecker() : base(typeof(ConditionHealthEffect))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionHealthEffect ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            if (Globals.IgnoreHealthEffectRequirements)
                __result = true;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionHealthEffect ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionHealthEffect condition = ___Condition;
            LogJsonDebug(condition);
        }
    }
}