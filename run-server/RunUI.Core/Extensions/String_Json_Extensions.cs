

using System.Text.Json;
using System.Text.Json.Serialization;

namespace RunUI
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class String_Json_Extensions
    {
        /// <summary>
        /// 将json字符串反序列化成类的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string str)
        {
            if (str.IsNullOrWhiteSpace())
            {
                throw new Exception($"Json反序列化`{str}`错误。");
            }
            try
            {
                return JsonSerializer.Deserialize<T>(str);
            }
            catch (Exception ex)
            {
                throw new Exception($"Json反序列化`{str}`错误。" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 将json字符串反序列化成类的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="enumToInt">枚举是否转为int</param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string str, bool enumToInt)
        {
            if (str.IsNullOrWhiteSpace())
            {
                throw new Exception($"Json反序列化`{str}`错误。");
            }
            var setting = new JsonSerializerOptions();
            if (enumToInt == false) setting.Converters.Add(new JsonStringEnumConverter());
            try
            {
                return JsonSerializer.Deserialize<T>(str, setting);
            }
            catch (Exception ex)
            {
                throw new Exception($"Json反序列化`{str}`错误。" + ex.Message, ex);
            }
        }


        /// <summary>
        /// 将json字符串反序列化成类的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="enumToInt">枚举是否转为int</param>
        /// <returns></returns>
        public static async ValueTask<T> JsonDeserialize<T>(this Stream stream, bool enumToInt)
        {
            var setting = new JsonSerializerOptions();
            if (enumToInt == false) setting.Converters.Add(new JsonStringEnumConverter());
            return await JsonSerializer.DeserializeAsync<T>(stream, setting);
        }

        /// <summary>
        /// 转换成匿名类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> JsonDeserializeAsyncEnumerable<T>(this Stream stream)
        {
            return JsonSerializer.DeserializeAsyncEnumerable<T>(stream);
        }

    }
}