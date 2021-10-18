using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NgTemplate.API.Extensions
{
    public static class EnumExtensions
{
    /// <summary>
    /// Get the Description from the DescriptionAttribute.
    /// </summary>
    /// <param name="enumValue">Enum value.</param>
    /// <returns>Description of the enum.</returns>
    public static string GetDescription(this Enum enumValue)
    {
        return enumValue.GetType()
                   .GetMember(enumValue.ToString())
                   .First()
                   .GetCustomAttribute<DescriptionAttribute>()?
                   .Description ?? string.Empty;
    }
}
}