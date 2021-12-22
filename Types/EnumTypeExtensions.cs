using System.ComponentModel;

namespace Types;

public static class EnumTypeExtensions
{
    public static string GetDescription(this Enum enumerationValue)
    {
        var type = enumerationValue.GetType();
        var memberInfo = type.GetMember(enumerationValue.ToString());
        if (memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs.Length > 0)
            {
                //Pull out the description value
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }
        //If we have no description attribute, just return the ToString of the enum
        return enumerationValue.ToString();
    }
}
