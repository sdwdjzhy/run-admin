using System.Linq.Expressions;

namespace RunUI
{
    /// <summary>
    /// </summary>
    public static class OrderByExtensions
    {
        /// <summary>
        /// 根据字段升序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="isAsc">升序</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool isAsc = true)
        {
            return QueryableMethod(source, propertyName, isAsc ? "OrderBy" : "OrderByDescending");
        }

        /// <summary>
        /// 根据字段降序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return QueryableMethod(source, propertyName, "OrderByDescending");
        }

        /// <summary>
        /// 根据字段升序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="methodName">OrderB，OrderByDescending，ThenBy，ThenByDescending</param>
        /// <returns></returns>
        public static IQueryable<T> QueryableMethod<T>(this IQueryable<T> source, string propertyName, string methodName)
        {
            if (source == null) throw new ArgumentNullException(nameof(source), "不能为空");
            if (propertyName.IsNullOrWhiteSpace()) return source;
            var parameter = Expression.Parameter(source.ElementType);
            var property = Expression.Property(parameter, propertyName);
            if (property == null) throw new ArgumentNullException(nameof(propertyName), "属性不存在");
            var lambda = Expression.Lambda(property, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), methodName, new[] { source.ElementType, property.Type }, source.Expression, Expression.Quote(lambda));
            return source.Provider.CreateQuery<T>(resultExpression);
        }

        /// <summary>
        /// 根据字段升序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="isAsc">升序</param>
        /// <returns></returns>
        public static IQueryable<T> ThenBy<T>(this IQueryable<T> source, string propertyName, bool isAsc = true)
        {
            return QueryableMethod(source, propertyName, isAsc ? "ThenBy" : "ThenByDescending");
        }

        /// <summary>
        /// 根据字段降序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable<T> ThenByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return QueryableMethod(source, propertyName, "ThenByDescending");
        }
    }
}