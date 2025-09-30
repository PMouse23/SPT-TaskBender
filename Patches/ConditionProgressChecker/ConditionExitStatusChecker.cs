using EFT.Quests;
using SPT.Reflection.Patching;

#nullable enable

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionExitStatusChecker : CheckerBasePatch
    {
        internal ConditionExitStatusChecker() : base(typeof(ConditionExitStatus))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionExitStatus ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            if (Globals.IgnoreExitStatus)
                __result = true;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionExitStatus ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionUpdater.Update(ref ___Condition);
            LogJsonDebug(___Condition);
        }
    }
}