using System.ComponentModel;
using System.Reflection;

namespace Business.Extensions;

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
}