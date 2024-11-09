using System.ComponentModel;
using System.Reflection;
using API.Resources.SystemData;

namespace API.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Get string in display-name data-anotation
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="enum"></param>
    /// <returns></returns>
    public static string ToDescriptionString<TEnum>(this TEnum @enum)
    {
        FieldInfo info = @enum.GetType().GetField(@enum.ToString());
        var attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes?[0].Description ?? @enum.ToString().ToLower();
    }

    public static string GetElementNameCodeMessage(this CodeMessage statusCode) =>
        Enum.GetName(typeof(CodeMessage), statusCode)?.TrimStart('_');
}