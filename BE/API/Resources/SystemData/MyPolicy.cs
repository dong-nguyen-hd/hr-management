namespace API.Resources.SystemData;

public sealed class MyPolicy
{
    #region Properties
    public const string Administrator = "administrator";
    public const string Editor = "editor";
    public const string Viewer = "viewer";
    #endregion

    #region Method
    public static bool IsValid(string policyName)
    {
        var fields = typeof(MyPolicy).GetFields();

        foreach (var field in fields)
            if (string.Equals(field.Name, policyName, StringComparison.OrdinalIgnoreCase))
                return true;

        return false;
    }
    #endregion
}
