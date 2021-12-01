namespace RunUI
{
    /// <summary>
    /// DoubleExtensions
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// 将double转换成Decimal
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this double d)
        {
            return Convert.ToDecimal(d);
        }

        /// <summary>
        /// 将decimal转换成Double
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double ToDouble(this decimal d)
        {
            return Convert.ToDouble(d);
        }
    }
}