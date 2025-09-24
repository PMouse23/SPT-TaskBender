using EFT.Quests;
using TaskBender.Models;

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
            return false;
        }

        private void updateConditionHit(ref ConditionHit ___Condition)
        {
            KillTarget killTarget = Globals.OverrideHitTarget;
            ConditionHit condition = ___Condition;
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