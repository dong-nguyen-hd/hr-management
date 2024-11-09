using System.ComponentModel;

namespace Business.Resources.Enums;

public enum eRole
{
    [Description(Role.Admin)]
    Admin = 1,

    [Description(Role.EditorQTNS)]
    EditorQTNS,

    [Description(Role.EditorQTDA)]
    EditorQTDA,

    [Description(Role.Viewer)]
    Viewer,

    [Description(Role.EditorKT)]
    EditorKT,
}

public class Role
{
    #region Role
    public const string Admin = "admin";
    public const string EditorQTNS = "editor-qtns";
    public const string EditorQTDA = "editor-qtda";
    public const string EditorKT = "editor-kt";
    public const string Viewer = "viewer";
    #endregion
}