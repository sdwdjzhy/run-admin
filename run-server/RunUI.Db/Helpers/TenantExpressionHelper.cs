using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public static class TenantExpressionHelper
    {
        public static Expression<Func<T, bool>> GetExpression<T>(string tenantId) where T : class, IBaseModel
        {
            var type = typeof(T);
            var Paramater = Expression.Parameter(type, "c");
            Expression filter = null;
            if (typeof(ITenantEntity).IsAssignableFrom(type))
            {
                var left = Expression.Property(Paramater, "TenantId");
                if (tenantId.IsNullOrWhiteSpace())
                {
                    throw new ArgumentNullException(nameof(tenantId), $"未传入参数tenantId");
                }
                var right = Expression.Constant(tenantId);
                filter = Expression.Equal(left, right);
            }
            if (typeof(IBaseModel).IsAssignableFrom(type))
            {
                var left = Expression.Property(Paramater, "Flag");
                var right = Expression.Constant(0);
                var result = Expression.GreaterThanOrEqual(left, right);

                filter = filter != null ? Expression.AndAlso(filter, result) : result;
            }
            if (filter != null)
                return Expression.Lambda<Func<T, bool>>(filter, Paramater);

            throw new InvalidOperationException($"当前类型【{type.Name}】,不能调用此方法获取where条件");
        }
    }
}
