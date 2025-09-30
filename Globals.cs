#nullable enable

using TaskBender.Models;

internal static class Globals
{
    public static bool Debug = false;

    public static double DistanceMultiplier = 1.0;
    public static bool IgnoreEnemyEquipmentRequirements = false;
    public static bool IgnoreEnemyHealthEffectRequirements = false;
    public static bool IgnoreEquipmentRequirements = false;
    public static bool IgnoreExitStatus = false;
    public static bool IgnoreHealthBuffRequirements = false;
    public static bool IgnoreHealthEffectRequirements = false;
    public static bool IgnoreInZoneRequirements = false;
    public static bool IgnoreLocationRequirements = false;
    public static bool IgnoreWeaponCategoryRequirements = false;
    public static bool IgnoreWeaponModRequirements = false;
    public static bool IgnoreWeaponRequirements = false;
    public static KillTarget OverrideHitTarget = KillTarget.AsInTask;
    public static bool OverrideKillTarget = true;
    public static bool OverrideShotsTarget = true;
}