using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RunUI
{
    /// <summary>
    /// 枚举和String 转换
    /// </summary>
    public class VCardEnumStringConverter : JsonConverterFactory
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
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(typeof(VCardEnumStringConverter<>).MakeGenericType(typeToConvert), BindingFlags.Instance | BindingFlags.Public, null, args: new object[] { null }, null);
        }
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class VCardEnumStringConverter<T> : JsonConverter<T>
    {
        /// <summary>
        /// 是否 将 不限、未知，在输出时输出成 空字符串
        /// </summary>
        private readonly IVCardEnumStringCustomerConverter<T> customerConverter = null;

        public VCardEnumStringConverter(IVCardEnumStringCustomerConverter<T> customerConverter)
        {
            this.customerConverter = customerConverter;
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var arr = reader.GetString();

            if (customerConverter?.IsNeedCustomer ?? false)
            {
                return customerConverter.GetEnum(arr);
            }
            else
            {
                var type = typeof(T);
                var newType = type;

                if (type.HasImplementedRawGeneric(typeof(Nullable<>)))
                {
                    newType = type.GenericTypeArguments[0];
                }
                var enumList = newType.GetEnumMap();

                if (arr.HasValue())
                {
                    var n = enumList.Where(i => i.Label.Equals(arr) || i.Label.Replace("_", "-").Trim("-").Equals(arr)).FirstOrDefault();
                    if (n != null)
                    {
                        var value = (T)Enum.Parse(newType, n.Label);

                        return GetEnum(value);
                    }
                }
                if (customerConverter?.IsNeedRemoveLimit ?? true)
                {
                    var n = enumList.Where(i => i.Label.Equals("不限") || i.Label.Equals("未知")).FirstOrDefault();
                    if (n != null)
                    {
                        var value = (T)Enum.Parse(newType, n.Label);
                        return GetEnum(value);
                    }
                }
                if (type.HasImplementedRawGeneric(typeof(Nullable<>)))
                {
                    return default;
                }
                throw new JsonException($"【{arr}】不能通过VCardEnumStringConverter转换成【{type.Name}】");
            }
        }

        private T GetEnum(T value)
        {
            if (customerConverter != null)
            {
                value = customerConverter.GetEnum(value);
            }
            return value;
        }

        /// <summary>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (customerConverter?.IsNeedCustomer ?? false)
            {
                var str = customerConverter.GetString(value);
                writer.WriteStringValue(str);
            }
            else
            {
                var str = value.ToStringExt();
                if (str.HasValue() && (customerConverter?.IsNeedRemoveLimit ?? true) && (str.EqualsIgnoreCase("不限") || str.EqualsIgnoreCase("未知")))
                {
                    writer.WriteStringValue("");
                }
                else if (str.IsNullOrWhiteSpace())
                {
                    writer.WriteStringValue("");
                }
                else
                {
                    var s = str.Replace("_", "-").Trim("-");
                    if (customerConverter != null)
                    {
                        s = customerConverter.GetString(s);
                    }
                    writer.WriteStringValue(s);
                }
            }
        }
    }
}