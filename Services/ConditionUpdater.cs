using EFT.Quests;
using TaskBender.Models;

#nullable enable

namespace TaskBender.Services
{
    internal class ConditionUpdater
    {
        public bool Update<T>(ref T condition) where T : Condition
        {
            if (condition is ConditionHit conditionHit)
            {
                this.updateConditionHit(ref conditionHit);
                return true;
            }
            else if (condition is ConditionCounterCreator conditionCounterCreator)
            {
                this.updateConditionCounterCreator(ref conditionCounterCreator);
                return true;
            }
            return false;
        }

        private static void updateDistanceRequirements(ConditionHit condition)
        {
            if (condition.distance == null)
                return;
            if (Globals.DistanceMultiplier == 1.0)
                return;
            double newValue = condition.distance.value * Globals.DistanceMultiplier;
            condition.distance.value = (int)newValue;
        }

        private static void updateEnemyEquipmentRequirements(ConditionHit condition)
        {
            if (Globals.IgnoreEnemyEquipmentRequirements == false)
                return;
            condition.enemyEquipmentInclusive = [];
            condition.enemyEquipmentExclusive = [];
        }

        private static void updateEnemyHealthEffectRequirements(ConditionHit condition)
        {
            if (Globals.IgnoreEnemyHealthEffectRequirements)
                condition.enemyHealthEffects = [];
        }

        private static void updateWeaponCategoryRequirements(ConditionHit condition)
        {
            if (Globals.IgnoreWeaponCategoryRequirements)
                condition.weaponCategories = [];
        }

        private static void updateWeaponModRequirements(ConditionHit condition)
        {
            if (Globals.IgnoreWeaponModRequirements == false)
                return;
            condition.weaponModsInclusive = [];
            condition.weaponModsExclusive = [];
        }

        private static void updateWeaponRequirements(ConditionHit condition)
        {
            if (Globals.IgnoreWeaponRequirements)
                condition.weapon = [];
        }

        private void updateConditionCounterCreator(ref ConditionCounterCreator conditionCounterCreator)
        {
            foreach (Condition condition in conditionCounterCreator.Conditions)
            {
                Condition updateCondition = condition;
                this.Update(ref updateCondition);
            }
        }

        private void updateConditionHit(ref ConditionHit condition)
        {
            KillTarget killTarget = Globals.OverrideHitTarget;
            if (condition is ConditionShots
                && Globals.OverrideShotsTarget == false)
                killTarget = KillTarget.AsInTask;
            if (condition is ConditionKills
                && Globals.OverrideKillTarget == false)
                killTarget = KillTarget.AsInTask;
            if (killTarget != KillTarget.AsInTask)
                condition.target = killTarget.ToString();
            updateWeaponRequirements(condition);
            updateWeaponCategoryRequirements(condition);
            updateWeaponModRequirements(condition);
            updateEnemyEquipmentRequirements(condition);
            updateEnemyHealthEffectRequirements(condition);
            updateDistanceRequirements(condition);
        }
    }
}