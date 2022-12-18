using System.ComponentModel;

namespace WZH.Common.Enums
{
    public static class EnumUtil
    {
        /// <summary>
        /// 获取枚举值的描述文本
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FetchDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
               (DescriptionAttribute[])fi.GetCustomAttributes(
               typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
    }
}