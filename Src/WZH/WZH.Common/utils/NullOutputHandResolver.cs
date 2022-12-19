using Newtonsoft.Json.Serialization;
namespace WZH.Common.utils
{
    /// <summary>
    /// null输出处理解析器
    /// </summary>
    public class NullOutputHandResolver : DefaultContractResolver
    {
        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberSerialization">序列化成员</param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties().Select(c =>
            {
                var jsonProperty = base.CreateProperty(c, memberSerialization);
                jsonProperty.ValueProvider = new NullOutputHandValueProvider(c);
                return jsonProperty;
            }).ToList();
        }
    }

    public class NullOutputHandValueProvider : IValueProvider
    {
        private readonly PropertyInfo _memberInfo;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="memberInfo"></param>
        public NullOutputHandValueProvider(PropertyInfo memberInfo)
        {
            _memberInfo = memberInfo;
        }

        /// <summary>
        /// 获取Value
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public object? GetValue(object target)
        {
            var result = _memberInfo.GetValue(target);
            //string
            if (_memberInfo.PropertyType == typeof(string) && result == null)
            {
                result = string.Empty;
            }
            //int或int?
            if ((_memberInfo.PropertyType == typeof(int) || _memberInfo.PropertyType == typeof(int?)) && result == null)
            {
                result = 0;
            }
            //long或long?
            if ((_memberInfo.PropertyType == typeof(long) || _memberInfo.PropertyType == typeof(long?)) && result == null)
            {
                result = 0L;
            }
            return result;
        }

        /// <summary>
        /// 设置Value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void SetValue(object target, object? value)
        {
            _memberInfo.SetValue(target, value);
        }
    }
}
