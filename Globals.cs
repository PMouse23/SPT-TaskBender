#nullable enable

using TaskBender.Models;

internal static class Globals
{
    public static bool Debug = false;

    public static bool IgnoreEquipmentRequirements = false;
    public static bool IgnoreExitStatus = false;
    public static bool IgnoreHealthBuffRequirements = false;
    public static bool IgnoreHealthEffectRequirements = false;
    public static bool IgnoreInZoneRequirements = false;
    public static bool IgnoreLocationRequirements = false;
    public static KillTarget OverrideHitTarget = KillTarget.AsInTask;
    public static bool OverrideKillTarget = true;
    public static bool OverrideShotsTarget = true;
}