using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace RunUI
{
    /// <summary>
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string? GetDisplayName(this PropertyInfo p)
        {
            var attr1 = p.GetCustomAttribute(typeof(DisplayNameAttribute));
            if (attr1 != null) return ((DisplayNameAttribute)attr1).DisplayName;
            var attr2 = p.GetCustomAttribute(typeof(DisplayAttribute));
            if (attr2 != null) return ((DisplayAttribute)attr2).Name;
            return p.Name;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static T? GetPropertyAttribute<T>(this PropertyInfo prop) where T : Attribute
        {
            return prop.GetPropertyAttributes<T>().FirstOrDefault();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static List<T> GetPropertyAttributes<T>(this PropertyInfo prop) where T : Attribute
        {
            return prop.GetCustomAttributes(true).OfType<T>().ToList();
        }

        /// <summary>
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsKey(this PropertyInfo property)
        {
            return property.GetPropertyAttribute<KeyAttribute>() != null;
        }

        /// <summary>
        /// 属性是否包含指定Attribute
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static bool IsPropertyHasAttribute<T>(this PropertyDescriptor prop) where T : Attribute
        {
            return prop.Attributes.OfType<T>().Any();
        }

        /// <summary>
        /// 属性是否包含指定Attribute
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static bool IsPropertyHasAttribute<T>(this PropertyInfo prop) where T : Attribute
        {
            return prop.GetCustomAttributes(true).Any(i => i is T);
        }
    }
}