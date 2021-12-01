namespace RunUI
{
    /// <summary>
    /// GuidExtensions
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// 返回去掉'-'的字符串
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static string ToShortString(this Guid g)
        {
            return g.ToString("N"); //.Replace("-", "");
        }
    }
}