namespace RunUI
{
    /// <summary>
    /// 自定义获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IVCardEnumStringCustomerConverter<T>
    {
        /// <summary>
        /// 是否需要自定义
        /// </summary>
        /// <returns></returns>
        bool IsNeedCustomer { get; set; }

        /// <summary>
        /// 是否需要将 不限、未知 =》 "" 
        /// </summary>
        /// <returns></returns>
        bool IsNeedRemoveLimit { get; set; }


        /// <summary>
        ///  自定义转义
        /// </summary>
        /// <returns></returns>
        string GetString(T value);

        /// <summary>
        ///  自定义转义
        /// </summary>
        /// <returns></returns>
        T GetEnum(string value);


        /// <summary>
        /// 对默认的数据进行修改
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string GetString(string str);
        /// <summary>
        /// 对默认的数据进行修改
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        T GetEnum(T str);


    }
} 