namespace MotoGrid.Common.Constants;

/// <summary>
/// Names of the user roles in the system.
/// </summary>
public static class RoleConstants
{
    public const string Administrator = "Administrator";
    public const string Moderator = "Moderator";
    public const string User = "User";

    public const string AdminOrModerator = Administrator + "," + Moderator;
}
