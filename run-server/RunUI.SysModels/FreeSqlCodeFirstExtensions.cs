using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI.SysModels
{
    public static class FreeSqlCodeFirstExtensions
    {
        /// <summary>
        /// 在codefirst模式下，创建表的索引
        /// </summary>
        /// <param name="fsql"></param>
        public static void AddIndexes(this IFreeSql fsql)
        {

            var tenantTypes = typeof(TSysTest).Assembly.GetTypes().Where(i => typeof(ITenantEntity).IsAssignableFrom(i)).ToList();
            tenantTypes.ForEach(type =>
            {
                fsql.CodeFirst.ConfigEntity(type, args =>
                {
                    args.Index("{tablename}_TenantId,IsDeleted_CreateTime_UpdateTime", "TenantId,IsDeleted,CreateTime desc,UpdateTime desc", false);
                });
            });
            var baseTypes = typeof(TSysTest).Assembly.GetTypes().Where(i => typeof(BaseModel).IsAssignableFrom(i))
                .Where(i => !tenantTypes.Contains(i)).ToList();
            baseTypes.ForEach(type =>
            {
                fsql.CodeFirst.ConfigEntity(type, args =>
                {
                    args.Index("{tablename}_IsDeleted_CreateTime_UpdateTime", "IsDeleted,CreateTime desc,UpdateTime desc", false);
                });
            });
        }
    }
}
