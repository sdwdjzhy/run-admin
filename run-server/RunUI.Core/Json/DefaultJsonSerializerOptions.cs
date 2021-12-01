
using System.Text.Json;

namespace RunUI
{
    /// <summary>
    /// </summary>
    public static class DefaultJsonSerializerOptions
    {
        /// <summary>
        /// 获取 json 的序列化选项
        /// </summary>
        /// <param name="IgnoreNullValues">是否忽略null值</param>
        /// <param name="datetimeFormat">DateTime的format</param>
        /// <param name="enumToInt">enum是否转成int型返回</param>
        /// <param name="escape">是否转义中文及标点</param>
        /// <param name="writeIndented">整齐打印</param>
        /// <returns></returns>
        public static JsonSerializerOptions GetJsonSerializerOptions(
            bool IgnoreNullValues = false,
            string datetimeFormat = AppConst.DateTimeUrlFormat,
            bool enumToInt = true,
            bool escape = false,
            bool writeIndented = false)
        {
            var option = new JsonSerializerOptions
            {
                // 属性的名称不变化
                PropertyNamingPolicy = null,
                // Dictionary的key不变化
                DictionaryKeyPolicy = null,
                WriteIndented = writeIndented,
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
                AllowTrailingCommas = true,

            };
            if (IgnoreNullValues)
            {
                option.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            }

            option.Converters.Add(new DateTimeConverter(datetimeFormat));
            option.Converters.Add(new DateTimeNullableConverter(datetimeFormat));

            if (!enumToInt)
            {
                option.Converters.Add(new VCardEnumStringConverter());
            }

            if (!escape)
            {
                option.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            }

            return option;
        }

        public static JsonSerializerOptions GetJsonSerializerOptions_Api()
        {
            var option = GetJsonSerializerOptions();
            option.Converters.Add(new EmptyStringConverter());
            return option;
        }

        public static void SetDefaultJsonSerializerOptions(JsonSerializerOptions option)
        {
            //option.PropertyNamingPolicy = MyJsonNamingPolicy.NoCaseName;
            //option.DictionaryKeyPolicy = MyJsonNamingPolicy.NoCaseName;
            option.PropertyNamingPolicy = null;
            option.DictionaryKeyPolicy = null;

            var datetimeFormat = AppConst.DateTimeUrlFormat;

            option.Converters.Add(new DateTimeConverter(datetimeFormat));
            option.Converters.Add(new DateTimeNullableConverter(datetimeFormat));

            option.Converters.Add(new VCardEnumStringConverter());
            // Uoption.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(nicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);
            option.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);
            option.Converters.Add(new EmptyStringConverter());
        }
    }
}
