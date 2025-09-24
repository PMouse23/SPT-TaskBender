using EFT;
using EFT.Quests;
using HarmonyLib;
using SPT.Common.Utils;
using SPT.Reflection.Patching;
using System;
using System.Linq;
using System.Reflection;
using TaskBender.Helpers;
using TaskBender.Services;

#nullable enable

namespace TaskBender.Patches.ConditionProgressChecker
{
    internal class CheckerBasePatch : ModulePatch
    {
        public static readonly ConditionUpdater ConditionUpdater = new ConditionUpdater();

        private Type conditionType;

        internal CheckerBasePatch(Type conditionType)
        {
            this.conditionType = conditionType;
        }

        public static void LogJsonDebug(Condition condition)
        {
            if (Globals.Debug)
                LogHelper.LogInfo($"{condition.GetType().Name} {Json.Serialize(condition)}");
        }

        public static void LogResultDebug(Condition condition, bool result)
        {
            if (Globals.Debug)
                LogHelper.LogInfo($"{condition.GetType().Name} result: {result}");
        }

        protected override MethodBase GetTargetMethod()
        {
            Type conditionProgressCheckerType = AccessTools.GetTypesFromAssembly(typeof(AbstractGame).Assembly).FirstOrDefault(this.isConditionProgressCheckerType);
            return AccessTools.FirstMethod(conditionProgressCheckerType, this.isTargetMethod);
        }

        private bool isConditionProgressCheckerType(Type type)
        {
            bool isConditionProgressChecker = type.IsSubclassOf(typeof(EFT.Quests.ConditionProgressChecker));
            if (isConditionProgressChecker == false)
                return false;
            FieldInfo? conditionFieldInfo = type.GetField("Condition", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            return conditionFieldInfo?.FieldType == this.conditionType;
        }

        private bool isTargetMethod(MethodInfo method)
        {
            return method.Name == nameof(EFT.Quests.ConditionProgressChecker.Test);
        }
    }
}