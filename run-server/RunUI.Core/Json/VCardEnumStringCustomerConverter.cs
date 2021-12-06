using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RunUI
{
    /// <summary>
    /// 枚举和String 转换
    /// </summary>
    public class VCardEnumStringCustomerConverter<T, Converter> : JsonConverterFactory
        where Converter : IVCardEnumStringCustomerConverter<T>, new()
    {
        /// <summary>
        /// </summary>
        /// <param name="typeToConvert"></param>
        /// <returns></returns>
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.HasImplementedRawGeneric(typeof(Nullable<>)) && typeToConvert.GenericTypeArguments[0].IsEnum)
            {
                return true;
            }
            else
                return typeToConvert.IsEnum;
        }

        /// <summary>
        /// </summary>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converter = Activator.CreateInstance<Converter>();
            return (JsonConverter?)Activator.CreateInstance(typeof(VCardEnumStringConverter<>).MakeGenericType(typeToConvert), BindingFlags.Instance | BindingFlags.Public, null, new object[] { converter }, null);
        }
    }
}