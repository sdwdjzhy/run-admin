using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RunUI
{
    /// <summary>
    /// </summary>
    public static class Type_Enum_Extensions
    {
        /// <summary>
        /// 自定义 获取枚举类型
        /// </summary>
        private static Func<Type, string, List<LabelValue<int>>> getEnumListCustom = null;

        /// <summary>
        /// 自定义 获取枚举类型
        /// <para>第一个参数：枚举类型</para>
        /// <para>第二个参数：当前使用场景</para>
        /// </summary>
        public static Func<Type, string, List<LabelValue<int>>> GetEnumListCustom { get => getEnumListCustom; set => getEnumListCustom = value; }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string GetEnumKeyDescription(this Type t, object v)
        {
            if (!t.IsEnum) throw new ArgumentException($"【{t.Name}】不是枚举类型");
            var name = Enum.GetName(t, v);
            var fi = t.GetField(name);
            if (fi == null) throw new ArgumentOutOfRangeException(nameof(v), $"不在枚举【{t.Name}】值中");

            //DescriptionAttribute
            {
                var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false).OfType<DescriptionAttribute>().FirstOrDefault();
                if (attributes != null)
                {
                    name = attributes.Description;
                }
            }
            // DisplayAttribute
            {
                var attributes = fi.GetCustomAttributes(typeof(DisplayAttribute), false).OfType<DisplayAttribute>().FirstOrDefault();
                if (attributes != null)
                {
                    name = attributes.Description.NullWhiteSpaceForDefault(attributes.Name);
                }
            }
            // DisplayNameAttribute
            {
                var attributes = fi.GetCustomAttributes(typeof(DisplayNameAttribute), false).OfType<DisplayNameAttribute>().FirstOrDefault();
                if (attributes != null)
                {
                    name = attributes.DisplayName;
                }
            }
            return name;
        }

        /// <summary>
        /// 获取枚举键值对
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetEnumKeyDescriptions(this Type t, string state = null)
        {
            if (!t.IsEnum) throw new ArgumentException($"【{t.Name}】不是枚举类型");

            var r = GetEnumListCustom?.Invoke(t, state);

            var result = new Dictionary<int, string>();
            if (r == null)
            {
                var arr = Enum.GetValues(t);
                foreach (int i in arr) result.Add(i, GetEnumKeyDescription(t, i));
            }
            else
            {
                result = r.GroupBy(i => i.Value).ToDictionary(i => i.Key, i => i.FirstOrDefault().Label);
            }

            return result;
        }

        /// <summary>
        /// 获取枚举键值对
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<LabelValue<int>> GetEnumMap(this Type t)
        {
            if (!t.IsEnum) throw new ArgumentException($"【{t.Name}】不是枚举类型");

            var result = GetEnumListCustom?.Invoke(t, null);

            if (result != null) return result;
            var arr = Enum.GetValues(t);
            return (from int i in arr select new LabelValue<int>(i, GetEnumKeyDescription(t, i))).ToList();
        }

        /// <summary>
        /// 是否在枚举中定义了
        /// </summary>
        /// <param name="t"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsEnumDefined(this Type t, int i)
        {
            if (!t.IsEnum) throw new ArgumentException($"【{t.Name}】不是枚举类型");

            return Enum.IsDefined(t, i);
        }
 
        /// <summary>
        /// 指示当前类型是否为 Nullable&lt;Enum&gt;
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public static bool IsEnumNullable(this Type T)
        {
            return T.HasImplementedRawGeneric(typeof(Nullable<>)) && T.GenericTypeArguments[0].IsEnum;
        }
    }
}