using System.ComponentModel;

namespace API.Resources.Enums;

public enum eOrder
{
    [Description("Component: Skill")]
    Skill = 1,

    [Description("Component: Project")]
    Project,

    [Description("Component: Work History")]
    WorkHistory,

    [Description("Component: Education")]
    Education,

    [Description("Component: Certificate")]
    Certificate,
}