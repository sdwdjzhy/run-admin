using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    /// <summary>
    /// </summary>
    public static class TypeExtensions
    {

        /// <summary>
        /// 是否存在某属性
        /// </summary>
        /// <param name="type"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static bool HasProperty(this Type type, string prop)
        {
            var p = type.GetProperty(prop);
            return p != null;
        }

        /// <summary>
        /// 是否在枚举中定义了
        /// </summary>
        /// <param name="t"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsDefined_Enum(this Type t, int i)
        {
            return Enum.IsDefined(t, i);
        }

        /// <summary>
        /// 查看某个类型是否可为空
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        public static bool IsNullableType(this Type theType)
        {
            return theType.IsGenericType && theType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }


        /// <summary>
        /// 判断指定的类型 <paramref name="type"/> 是否是指定泛型类型的子类型，或实现了指定泛型接口。
        /// </summary>
        /// <param name="type">需要测试的类型。</param>
        /// <param name="generic">泛型接口类型，传入 typeof(IXxx&lt;&gt;)</param>
        /// <returns>如果是泛型接口的子类型，则返回 true，否则返回 false。</returns>
        public static bool HasImplementedRawGeneric(this Type? type, Type? generic)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (generic == null) throw new ArgumentNullException(nameof(generic));

            // 测试接口。
            var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
            if (isTheRawGenericType) return true;

            // 测试类型。
            while (type != null && type != typeof(object))
            {
                isTheRawGenericType = IsTheRawGenericType(type);
                if (isTheRawGenericType) return true;
                type = type.BaseType;
            }

            // 没有找到任何匹配的接口或类型。
            return false;

            // 测试某个类型是否是指定的原始接口。
            bool IsTheRawGenericType(Type test)
                => generic == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
        }
    }
}
