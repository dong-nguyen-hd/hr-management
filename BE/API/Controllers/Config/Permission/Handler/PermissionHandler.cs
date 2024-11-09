using API.Controllers.Config.Permission.Requirement;

namespace API.Controllers.Config.Permission.Handler;

public class PermissionHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        var pendingRequirements = context.PendingRequirements.ToList();

        foreach (var requirement in pendingRequirements)
        {
            if (requirement is PermissionRequirement permissionRequirement)
                if (IsValid(context.User, permissionRequirement.Permissions))
                    context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    #region Private work

    private static bool IsValid(ClaimsPrincipal user, List<string> permissions)
    {
        List<Claim> roleClaims = user.FindAll(ClaimTypes.Role).ToList();

        if (roleClaims.Count == 0)
            return false;

        foreach (var role in roleClaims)
            if (!string.IsNullOrEmpty(role.Value) && permissions.Contains(role.Value))
                return true;

        return false;
    }

    #endregion
}