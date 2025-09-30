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
            //TODO add settings.
            condition.weapon = [];
            condition.weaponCategories = [];
            condition.weaponModsInclusive = [];
            condition.weaponModsExclusive = [];
            condition.enemyEquipmentInclusive = [];
            condition.enemyEquipmentExclusive = [];
            condition.enemyHealthEffects = [];
            if (condition.distance != null)
                condition.distance.value = 0;
        }
    }
}