namespace RunUI
{
    public interface ITenantProvider
    {
        /// <summary>
        /// 获取租户编号
        /// </summary>
        /// <returns></returns>
        Task<string> GetTenantId();

        /// <summary>
        /// 获取租户信息
        /// </summary>
        /// <returns></returns>
        Task<Tenant> GetTenant();
    }
}