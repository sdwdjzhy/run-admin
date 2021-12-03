using Newtonsoft.Json;

namespace RunUI
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class String_JsonNet_Extensions
    {
        public static object DeserializeObject(this string str)
        {
            try
            {
                return JsonConvert.DeserializeObject(str);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Json反序列化`{str}`错误。", e);
            }
        }
    }
}