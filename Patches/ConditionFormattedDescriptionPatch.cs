using EFT.Quests;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Linq;
using System.Reflection;
using TaskBender.Helpers;
using TaskBender.Services;

namespace TaskBender.Patches
{
    internal class ConditionFormattedDescriptionPatch : ModulePatch
    {
        private static ConditionUpdater conditionUpdater = new ConditionUpdater();

        public static void LogResultDebug(Condition condition, string result)
        {
            if (Globals.Debug)
                LogHelper.LogInfo($"{condition.GetType().Name} result: {result}");
        }

        [PatchPostfix]
        public static void PatchPostfix(Condition __instance, ref string __result)
        {
            if (__instance == null)
                return;
            try
            {
                Condition condition = __instance;
                if (condition is ConditionCounterCreator conditionCounterCreator
                    && conditionCounterCreator.Conditions.Any(isUpdatableCondition))
                {
                    conditionCounterCreator.Conditions.Remove(condition);
                    __result = conditionCounterCreator.LocalizeDescription();
                }
                if (conditionUpdater.Update(ref condition))
                {
                    condition.DynamicLocale = true;
                    //HACK two times so the message is correctly modified.
                    __result = condition.FormattedDescription;
                    __result = condition.FormattedDescription;
                }
            }
            catch (Exception exception)
            {
                LogHelper.LogExceptionToConsole(exception);
            }
            finally
            {
                LogResultDebug(__instance, __result);
            }
        }

        [PatchPrefix]
        public static void PatchPrefix(ref Condition __instance)
        {
            if (__instance == null)
                return;
        }

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.FirstMethod(typeof(Condition), this.isTargetMethod);
        }

        private static bool isUpdatableCondition(Condition condition)
        {
            return condition is ConditionHit;
        }

        private bool isTargetMethod(MethodBase method)
        {
            return method.Name == $"get_{nameof(Condition.FormattedDescription)}";
        }
    }
}