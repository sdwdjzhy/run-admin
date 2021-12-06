using System.ComponentModel.DataAnnotations;

namespace RunUI
{
    public interface ITenantEntity
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [Display(Name = "租户编号")]
        string TenantId { get; set; }
    }
}