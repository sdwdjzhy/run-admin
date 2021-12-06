using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    /// <summary>
    /// 构建Where条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class WhereHelper<T>
        where T : class
    {
        public ParameterExpression Paramater { get; }
        private readonly Type type;
        private Expression filter;

        /// <summary>
        /// 构造器
        /// </summary>
        public WhereHelper()
        {
            Paramater = Expression.Parameter(typeof(T), "c");
            filter = null;
            type = typeof(T);
        }

        public void And(Expression result)
        {
            filter = filter != null ? Expression.AndAlso(filter, result) : result;
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void ArrayContains<K>(PropertyInfo property, ICollection<K> value)
        {
            var left = Expression.Constant(value);
            var right = Expression.Property(Paramater, property);
            var result = Expression.Call(left, typeof(ICollection<K>).GetMethod("Contains"), right);
            And(result);
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="t"></param>
        public void ArrayContains(PropertyInfo property, object value, Type t)
        {
            var propertyType = property.PropertyType;
            if (t.GenericTypeArguments.Length > 0 && propertyType == t.GenericTypeArguments[0])//当类型相同优先走Contains
            {
                var left = Expression.Constant(value);
                var right = Expression.Property(Paramater, property);
                var result = Expression.Call(left, t.GetMethod("Contains"), right);
                And(result);
            }
            else
            {
                //var listType = typeof(List<>).MakeGenericType(propertyType);
                //var left = Expression.Constant(value, listType);
                Expression a;
                {//先判断不为空 
                    var left = Expression.Property(Paramater, property);
                    var right = Expression.Constant(null, property.PropertyType);
                    var result = Expression.NotEqual(left, right);
                    a = result;
                    //And(result);
                }
                {//再Contains
                    var left = Expression.Constant(value);
                    var right = Expression.Property(Paramater, property);
                    right = Expression.Property(right, "Value");
                    Expression result = Expression.Call(left, t.GetMethod("Contains"), right);
                    result = Expression.AndAlso(a, result);
                    And(result);
                }
            }
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void Contains(PropertyInfo property, string value)
        {
            var left = Expression.Property(Paramater, property);
            var right = Expression.Constant(value);
            var result = Expression.Call(left, typeof(string).GetMethod("Contains", new[] { typeof(string) }), right);
            And(result);
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void EndsWith(PropertyInfo property, string value)
        {
            var left = Expression.Property(Paramater, property);
            var right = Expression.Constant(value);
            var result = Expression.Call(left, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), right);
            And(result);
        }

        /// <summary>
        /// 判断相等
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void Equal<TValue>(PropertyInfo property, TValue value)
        {

            var left = Expression.Property(Paramater, property);
            var right = Expression.Constant(value);
            var result = Expression.Equal(left, right);
            And(result);
        }
        /// <summary>
        /// 判断不相等
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void NotEqual<TValue>(PropertyInfo property, TValue value)
        {

            var left = Expression.Property(Paramater, property);
            var right = Expression.Constant(value);
            var result = Expression.NotEqual(left, right);
            And(result);
        }
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True()
        {
            return i => true;
        }
        /// <summary>
        /// 获取最终的表达式
        /// </summary>
        /// <returns></returns>
        public Expression<Func<T, bool>> GetExpression()
        {
            return filter == null ? True() : Expression.Lambda<Func<T, bool>>(filter, Paramater);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void GreaterThan<TValue>(PropertyInfo property, TValue value)
        {
            var left = Expression.Property(Paramater, property);
            var right = Expression.Constant(value, property.PropertyType);
            var result = Expression.GreaterThan(left, right);
            And(result);
        }

        /// <summary>
        /// 大于或等于
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="liftToNull"></param>
        /// <param name="method"></param>
        public void GreaterThanOrEqual<TValue>(PropertyInfo property, TValue value, bool liftToNull = false, MethodInfo method = null)
        {
            var left = Expression.Property(Paramater, property);
            var right = Expression.Constant(value, property.PropertyType);
            var result = Expression.GreaterThanOrEqual(left, right, liftToNull, method);
            And(result);
        }

        /// <summary>
        /// 大于或等于
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="liftToNull"></param>
        /// <param name="method"></param>
        public void GreaterThanOrEqual_String<TValue>(PropertyInfo property, TValue value, bool liftToNull = false, MethodInfo method = null)
        {
            var left = Expression.Property(Paramater, property);
            var right = Expression.Constant(value);

            var result = Expression.GreaterThanOrEqual(Expression.Call(null, method, new Expression[2] { left, right }), Expression.Constant(0, typeof(int)));

            And(result);
        }



        /// <summary>
        /// 小于或等于
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="liftToNull"></param>
        /// <param name="method"></param>
        public void LessThanOrEqual_String<TValue>(PropertyInfo property, TValue value, bool liftToNull = false, MethodInfo method = null)
        {
            var left = Expression.Property(Paramater, property);
            var right = Expression.Constant(value);

            var result = Expression.LessThanOrEqual(Expression.Call(null, method, new Expression[2] { left, right }), Expression.Constant(0, typeof(int)));

            And(result);
        }



        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void LessThan<TValue>(PropertyInfo property, TValue value)
        {
            var prop = property;
            var left = Expression.Property(Paramater, prop);
            var right = Expression.Constant(value, property.PropertyType);
            var result = Expression.LessThan(left, right);
            And(result);
        }

        /// <summary>
        /// 小于或等于
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="liftToNull"></param>
        /// <param name="method"></param>
        public void LessThanOrEqual<TValue>(PropertyInfo property, TValue value, bool liftToNull = false, MethodInfo method = null)
        {
            var prop = property;
            var left = Expression.Property(Paramater, prop);
            var right = Expression.Constant(value, property.PropertyType);
            var result = Expression.LessThanOrEqual(left, right, liftToNull, method);
            And(result);
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void StartsWith(PropertyInfo property, string value)
        {
            var left = Expression.Property(Paramater, property);
            var right = Expression.Constant(value);
            var result = Expression.Call(left, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), right);
            And(result);
        }
    }
}
