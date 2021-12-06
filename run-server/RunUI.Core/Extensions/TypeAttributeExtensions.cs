namespace RunUI
{
    /// <summary>
    /// 特性辅助类
    /// </summary>
    public static class TypeAttributeExtensions
    {
        #region 类型的

        /// <summary>
        /// 获取某类型的某种特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<T> GetTypeAttributes<T>(this Type type)
            where T : Attribute
        {
            var list = new List<T>();
            var attrs = type.GetCustomAttributes(true);
            list.AddRange(attrs.OfType<T>().ToList());
            return list;
        }

        /// <summary>
        /// 是否有指定的特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTypeHasAttribute<T>(this Type type)
            where T : Attribute
        {
            return GetTypeAttributes<T>(type).Any();
        }

        #endregion 类型的

        #region 属性的

        /// <summary>
        /// 类的属性里，包含指定Attribute的属性名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string[] GetAttributeProperties<T>(this Type type)
            where T : Attribute
        {
            var list = new List<string>();

            var props = type.GetProperties();
            list.AddRange(from prop in props
                          where prop.GetCustomAttributes(true).OfType<T>().Any()
                          select prop.Name);
            return list.ToArray();
        }

        /// <summary>
        /// 类的属性里，包含指定Attribute的属性名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAttributeProperty<T>(this Type type)
            where T : Attribute
        {
            var props = type.GetProperties();
            foreach (var prop in props)
                if (prop.GetCustomAttributes(true).OfType<T>().Any())
                    return prop.Name;
            return null;
        }

        /// <summary>
        /// 获取某类型的某属性的指定特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static List<T> GetPropertyAttributes<T>(this Type type, string propName)
            where T : Attribute
        {
            if (propName.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(propName), "请输入属性名");
            var list = new List<T>();

            var prop = type.GetProperty(propName);
            if (prop == null)
                throw new ArgumentException($"输入属性名【{propName}】不是此类型【{type.FullName}】的属性");
            list.AddRange(prop.GetPropertyAttributes<T>());
            return list;
        }

        #endregion 属性的
    }
}