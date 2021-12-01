using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RunUI
{
    /// <summary>
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// 扩展Between 操作符 使用 var query = db.People.Between(person =&gt; person.Age, 18, 21);
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static IQueryable<TSource> Between<TSource, TKey>
        (this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            TKey low, TKey high) where TKey : IComparable<TKey>
        {
            Expression key = Expression.Invoke(keySelector,
                keySelector.Parameters.ToArray());
            Expression lowerBound = Expression.GreaterThanOrEqual
                (key, Expression.Constant(low));
            Expression upperBound = Expression.LessThanOrEqual
                (key, Expression.Constant(high));
            Expression and = Expression.AndAlso(lowerBound, upperBound);
            var lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);
            return source.Where(lambda);
        }

        /// <summary>
        /// 根据类的字段名，返回该字段对映的表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Expression<Func<T, object>> CreateExpression<T>(this string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            return Expression.Lambda<Func<T, object>>(property, parameter);
        }

        /// <summary>
        /// 根据类的字段名，返回该字段对映的表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Expression<Func<T, K>> CreateExpression<T, K>(this string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            return Expression.Lambda<Func<T, K>>(property, parameter);
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Expression CreateExpression(Type type, string propertyName)
        {
            var parameter = Expression.Parameter(type);
            var property = Expression.Property(parameter, propertyName);
            return Expression.Lambda(property, parameter);
        }

        /// <summary>
        /// Creates a predicate that evaluates to false.
        /// </summary>
        public static Expression<Func<T, bool>> False<T>()
        {
            return param => false;
        }

        /// <summary>
        /// 获取指定的属性名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetPropertyKName<T, K>(this Expression<Func<T, K>> expr)
        {
            return expr.GetPropertyNames().Join(",");
        }

        /// <summary>
        /// 获取指定的属性名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetPropertyName<T>(this Expression<Func<T, object>>[] expr)
        {
            if (expr == null || !expr.Any()) return "";

            var list = new List<string>();
            foreach (var item in expr) list.AddRange(item.GetPropertyNames());
            return list.Join(",");
        }

        /// <summary>
        /// 获取指定的属性名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        [Obsolete("使用 GetPropertyKName 代替")]
        public static string GetPropertyName<T>(this Expression<Func<T, object>> expr)
        {
            return expr.GetPropertyKName();
        }

        /// <summary>
        /// 获取指定的属性名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string[] GetPropertyNames<T, K>(this Expression<Func<T, K>> expr)
        {
            var rtn = "";
            if (expr == null) return Array.Empty<string>();
            switch (expr.Body)
            {
                case UnaryExpression exp:
                    rtn = ((MemberExpression)exp.Operand).Member.Name;
                    break;

                case MemberExpression memberExpression:
                    rtn = memberExpression.Member.Name;
                    break;

                case ParameterExpression parameterExpression:
                    rtn = parameterExpression.Type.Name;
                    break;

                case NewExpression newExpression:
                    {
                        var c = newExpression.Members;
                        return c == null ? Array.Empty<string>() : c.Select(i => i.Name).ToArray();
                    }
                case MemberInitExpression memberInitExpression:
                    {
                        var c = memberInitExpression.Bindings;
                        return c.Select(i => i.Member.Name).ToArray();
                    }
            }

            return new[] { rtn };
        }

        /// <summary>
        /// 获取指定的属性名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string[] GetPropertyNames<T, K>(this Expression<Func<T, K>>[] expr)
        {
            if (expr == null || !expr.Any()) return Array.Empty<string>();

            var list = new List<string>();
            foreach (var item in expr) list.AddRange(item.GetPropertyNames());
            return list.ToArray();
        }

        /// <summary>
        /// Creates a predicate that evaluates to true.
        /// </summary>
        public static Expression<Func<T, bool>> True<T>()
        {
            return param => true;
        }
    }
}