using System.ComponentModel;
using System.Linq;

namespace STT.Application.Helpers
{
    public static class BasicDataTypeHelper
    {
        public static string? GetDescription<T>(this T genericEnum)
            where T : struct
        {
            var enumType = typeof(T);

            if (!enumType.IsEnum)
            {
                return null;
            }

            var memberInfo = enumType.GetMember(genericEnum.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Count() > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return genericEnum.ToString();
        }
    }
}