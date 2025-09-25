using EFT.Quests;
using SPT.Reflection.Patching;

#nullable enable

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class ConditionEquipmentChecker : CheckerBasePatch
    {
        internal ConditionEquipmentChecker() : base(typeof(ConditionEquipment))
        { }

        [PatchPostfix]
        public static void PatchPostfix(ConditionEquipment ___Condition, ref bool __result)
        {
            if (___Condition == null)
                return;
            if (Globals.IgnoreEquipmentRequirements)
                __result = true;
            LogResultDebug(___Condition, __result);
        }

        [PatchPrefix]
        public static void PatchPrefix(ConditionEquipment ___Condition)
        {
            if (___Condition == null)
                return;
            ConditionEquipment condition = ___Condition;
            LogJsonDebug(condition);
        }
    }
}