namespace API.Controllers.Config.Permission.Requirement;

public sealed class PermissionRequirement : IAuthorizationRequirement
{
    #region Properties

    public List<string> Permissions { get; init; }

    #endregion

    #region Constructor

    public PermissionRequirement(List<string> permissions) =>
        Permissions = permissions;

    #endregion
}