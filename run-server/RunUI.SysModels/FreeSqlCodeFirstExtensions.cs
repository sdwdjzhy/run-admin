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
            var tenantTypes = typeof(FreeSqlCodeFirstExtensions).Assembly.GetTypes().Where(i => typeof(ITenantEntity).IsAssignableFrom(i)).ToList();
            tenantTypes.ForEach(type =>
            {
                fsql.CodeFirst.ConfigEntity(type, args =>
                {
                    args.Index("{tablename}_TenantId_Flag_CreateTime_UpdateTime", "TenantId,Flag desc,CreateTime desc,UpdateTime desc", false);
                });
            });
            var baseTypes = typeof(FreeSqlCodeFirstExtensions).Assembly.GetTypes().Where(i => typeof(IBaseModel).IsAssignableFrom(i))
                .Where(i => !tenantTypes.Contains(i)).ToList();
            baseTypes.ForEach(type =>
            {
                fsql.CodeFirst.ConfigEntity(type, args =>
                {
                    args.Index("{tablename}_Flag_CreateTime_UpdateTime", "Flag desc,CreateTime desc,UpdateTime desc", false);
                });
            });
        }
    }
}