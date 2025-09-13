using BepInEx;
using BepInEx.Configuration;
using EFT;
using EFT.Quests;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaskBender.Helpers;
using TaskBender.Patches.ConditionProgressChecker;

#nullable enable

namespace TaskBender
{
    [BepInPlugin("com.KnotScripts.TaskBender", "TaskBender", "0.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        private ConfigEntry<bool> debug;

        private void Awake()
        {
            try
            {
                LogHelper.Logger = this.Logger;

                this.setConfigurables();
                this.enablePatches();
            }
            catch (Exception exception)
            {
                LogHelper.LogException(exception);
            }
        }

        private void enablePatches()
        {
            this.printAllTypes();

            //new ConditionBlockChecker().Enable();
            //new ConditionCounterCreatorChecker().Enable();
            new ConditionEquipmentChecker().Enable();
            new ConditionExitNameChecker().Enable();
            new ConditionExitStatusChecker().Enable();
            new ConditionHealthBuffChecker().Enable();
            //new ConditionHealthChecker().Enable();
            new ConditionHealthEffectChecker().Enable();
            new ConditionHitChecker().Enable();
            new ConditionInZoneChecker().Enable();
            new ConditionLaunchFlareChecker().Enable();
            //new ConditionLevelChecker().Enable();
            new ConditionLocationChecker().Enable();
            //new ConditionMultipleTargetsChecker().Enable();
            //new ConditionOneTargetChecker().Enable();
            //new ConditionPrestigeLevelChecker().Enable();
            //new ConditionQuestTimeChecker().Enable();
            //new ConditionTimeChecker().Enable();
            //new ConditionTransitionLocationChecker().Enable();
            //new ConditionUnderArtilleryFireChecker().Enable();
            new ConditionUseItemChecker().Enable();
        }

        private void global_SettingChanged(object sender, EventArgs e)
        {
            this.setGlobalSettings();
        }

        private bool isConditionProgressCheckerType(Type type)
        {
            return type.IsSubclassOf(typeof(ConditionProgressChecker));
        }

        private void printAllTypes()
        {
            if (Globals.Debug == false)
                return;
            IEnumerable<Type> types = AccessTools.GetTypesFromAssembly(typeof(AbstractGame).Assembly).Where(this.isConditionProgressCheckerType);
            foreach (Type type in types)
            {
                string typeName = type.Name;
                FieldInfo? conditionFieldInfo = type.GetField("Condition", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                string conditionTypeName = conditionFieldInfo?.FieldType.Name ?? string.Empty;
                LogHelper.LogInfo($"Type: {typeName}, ConditionType: {conditionTypeName}");
            }
        }

        private void setConfigurables()
        {
            this.debug = this.Config.Bind("General", "Debug", false, "Debug");
            this.debug.SettingChanged += this.global_SettingChanged;

            this.setGlobalSettings();
        }

        private void setGlobalSettings()
        {
            Globals.Debug = this.debug.Value;
        }
    }
}