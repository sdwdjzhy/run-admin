using FreeSql;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq.Expressions;

namespace RunUI
{
    public abstract class BaseCrudController<T> : BaseController where T : class, IBaseModel
    {
        protected ITenantProvider TenantProvider { get; private set; } = null;
        protected string TenantId { get; private set; } = null;
        protected IFreeSql Orm { get; private set; } = null;
        protected IBaseRepository<T> Repository { get; set; } = null;

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Orm = this.GetService<IFreeSql>();
            TenantProvider = this.GetService<ITenantProvider>();
            if (TenantProvider != null)
            {
                TenantId = await TenantProvider.GetTenantId();
            }

            Repository = GetRepository<T>();

            await base.OnActionExecutionAsync(context, next);
        }

        protected virtual IBaseRepository<K> GetRepository<K>() where K : class, IBaseModel
        {
            var expression = TenantExpressionHelper.GetExpression<K>(TenantId);
            return Orm.GetRepository(expression);
        }

        //[HttpPost]
        //public virtual async Task<PagedListResult<T>> List()
        //{
        //    var qg = new QueryHelper<T>(Request.Body);

        //    var pagedList = await Repository.ToPagedListAsync(qg, i => i.UpdateTime, true);

        //    #region  填充属性中的Select
        //    var select = new List<LabelValueGroup>();
        //    {
        //        var type = typeof(T);
        //        var properties = type.GetProperties();
        //        foreach (var property in properties)
        //        {
        //            if (property.PropertyType.IsEnum || property.PropertyType.IsEnumNullable())
        //            {
        //                var enumType = property.PropertyType.IsEnum ? property.PropertyType : property.PropertyType.GetGenericArguments()[0];
        //                var maps = enumType.GetEnumMap();
        //                select.Add(new LabelValueGroup
        //                {
        //                    Children = maps,
        //                    Name = property.Name,
        //                });
        //            }
        //            else if(property.IsPropertyHasAttribute<ForeignEnumAttribute>(out var attr))
        //            {

        //            }
        //        }

        //    }
        //    #endregion

        //}
    }
}
