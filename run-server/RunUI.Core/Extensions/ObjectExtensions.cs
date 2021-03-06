using System.Text.Json;

namespace RunUI
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// object转换成string（若object为null则返回string.Empty）
        /// </summary>
        /// <param name="source">数据源</param>
        public static string ToStringExt<T>(this T source)
        {
            return source?.ToString() ?? "";
        }

        /// <summary>
        /// 将类的对象json格式化
        /// </summary>
        /// <param name="o">需要json序列化的对象</param>
        /// <param name="setting">json序列化配置</param>
        /// <returns></returns>
        public static string JsonSerialize<T>(this T o, JsonSerializerOptions setting = null)
        {
            if (o == null) return "";

            setting ??= DefaultJsonSerializerOptions.GetJsonSerializerOptions();

            return JsonSerializer.Serialize(o, setting);
        }
    }
}