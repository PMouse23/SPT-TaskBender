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
using TaskBender.Models;
using TaskBender.Patches;
using TaskBender.Patches.ConditionProgressChecker;

#nullable enable

namespace TaskBender
{
    [BepInPlugin("com.KnotScripts.TaskBender", "TaskBender", "0.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        private ConfigEntry<bool> debug;
        private ConfigEntry<double> distanceMultiplier;
        private ConfigEntry<bool> ignoreEnemyEquipmentRequirements;
        private ConfigEntry<bool> ignoreEnemyHealthEffectRequirements;
        private ConfigEntry<bool> ignoreEquipmentRequirements;
        private ConfigEntry<bool> ignoreExitStatus;
        private ConfigEntry<bool> ignoreHealthBuffRequirements;
        private ConfigEntry<bool> ignoreHealthEffectRequirements;
        private ConfigEntry<bool> ignoreInZoneRequirements;
        private ConfigEntry<bool> ignoreLocationRequirements;
        private ConfigEntry<bool> ignoreWeaponCategoryRequirements;
        private ConfigEntry<bool> ignoreWeaponModRequirements;
        private ConfigEntry<bool> ignoreWeaponRequirements;
        private ConfigEntry<KillTarget> overrideHitTarget;
        private ConfigEntry<bool> overrideKillTarget;
        private ConfigEntry<bool> overrideShotsTarget;

        private ConfigEntry<T> addConfigurable<T>(string section, string key, T defaultValue, string description)
        {
            ConfigEntry<T> configEntry = this.Config.Bind(section, key, defaultValue, description);
            configEntry.SettingChanged += this.global_SettingChanged;
            return configEntry;
        }

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

            new ConditionFormattedDescriptionPatch().Enable();
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
            this.debug = this.addConfigurable(section: "General", key: "Debug", defaultValue: false, description: "Debug");
            this.ignoreEquipmentRequirements = this.addConfigurable(section: "Ignore", key: "IgnoreEquipmentRequirements", defaultValue: false, description: "Ignore equipment requirements.");
            this.ignoreExitStatus = this.addConfigurable(section: "Ignore", key: "IgnoreExitStatus", defaultValue: false, description: "Ignore exit status.");
            this.ignoreHealthBuffRequirements = this.addConfigurable(section: "Ignore", key: "IgnoreHealthBuffRequirements", defaultValue: false, description: "Ignore health buff requirements.");
            this.ignoreHealthEffectRequirements = this.addConfigurable(section: "Ignore", key: "IgnoreHealthEffectRequirements", defaultValue: false, description: "Ignore health effect requirements.");
            this.ignoreInZoneRequirements = this.addConfigurable(section: "Ignore", key: "IgnoreInZoneRequirements", defaultValue: false, description: "Ignore in zone requirements.");
            this.ignoreLocationRequirements = this.addConfigurable(section: "Ignore", key: "IgnoreLocationRequirements", defaultValue: false, description: "Ignore location requirements.");
            this.overrideHitTarget = this.addConfigurable(section: "ShootAndKill", key: "OverrideHitTarget", defaultValue: KillTarget.AsInTask, description: "Override targets for quests.");
            this.overrideKillTarget = this.addConfigurable(section: "ShootAndKill", key: "OverrideKillTarget", defaultValue: false, description: "Use targets override for kill quests.");
            this.overrideShotsTarget = this.addConfigurable(section: "ShootAndKill", key: "OverrideShotsTarget", defaultValue: false, description: "Use targets override for shots quests.");
            this.ignoreWeaponRequirements = this.addConfigurable(section: "ShootAndKill", key: "IgnoreWeaponRequirements", defaultValue: false, description: "Ignore weapon requirements.");
            this.ignoreWeaponCategoryRequirements = this.addConfigurable(section: "ShootAndKill", key: "IgnoreWeaponCategoryRequirements", defaultValue: false, description: "Ignore weapon category requirements.");
            this.ignoreWeaponModRequirements = this.addConfigurable(section: "ShootAndKill", key: "IgnoreWeaponModRequirements", defaultValue: false, description: "Ignore weapon mod requirements.");
            this.ignoreEnemyEquipmentRequirements = this.addConfigurable(section: "ShootAndKill", key: "IgnoreEnemyEquipmentRequirements", defaultValue: false, description: "Ignore enemy equipment requirements.");
            this.ignoreEnemyHealthEffectRequirements = this.addConfigurable(section: "ShootAndKill", key: "IgnoreEnemyHealthEffectRequirements", defaultValue: false, description: "Ignore enemy health effect requirements.");
            this.distanceMultiplier = this.addConfigurable(section: "ShootAndKill", key: "DistanceMultiplier", defaultValue: 1.0, description: "Distance multiplier.");
            this.setGlobalSettings();
        }

        private void setGlobalSettings()
        {
            Globals.Debug = this.debug.Value;
            Globals.IgnoreEquipmentRequirements = this.ignoreEquipmentRequirements.Value;
            Globals.IgnoreExitStatus = this.ignoreExitStatus.Value;
            Globals.IgnoreHealthBuffRequirements = this.ignoreHealthBuffRequirements.Value;
            Globals.IgnoreHealthEffectRequirements = this.ignoreHealthEffectRequirements.Value;
            Globals.IgnoreInZoneRequirements = this.ignoreInZoneRequirements.Value;
            Globals.IgnoreLocationRequirements = this.ignoreLocationRequirements.Value;
            Globals.OverrideHitTarget = this.overrideHitTarget.Value;
            Globals.OverrideKillTarget = this.overrideKillTarget.Value;
            Globals.OverrideShotsTarget = this.overrideShotsTarget.Value;
            Globals.IgnoreWeaponRequirements = this.ignoreWeaponRequirements.Value;
            Globals.IgnoreWeaponCategoryRequirements = this.ignoreWeaponCategoryRequirements.Value;
            Globals.IgnoreWeaponModRequirements = this.ignoreWeaponModRequirements.Value;
            Globals.IgnoreEnemyEquipmentRequirements = this.ignoreEnemyEquipmentRequirements.Value;
            Globals.IgnoreEnemyHealthEffectRequirements = this.ignoreEnemyHealthEffectRequirements.Value;
            Globals.DistanceMultiplier = this.distanceMultiplier.Value;
        }
    }
}