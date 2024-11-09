namespace Business.Resources.Attributes;

/// <summary>
/// Chức năng: loại bỏ các thông tin nhạy cảm string-type trong DTO
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class SensitiveDataAttribute : Attribute
{
}