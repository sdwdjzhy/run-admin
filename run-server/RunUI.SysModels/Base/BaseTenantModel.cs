using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RunUI
{
    public abstract class BaseTenantModel : BaseModel, ITenantEntity
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [MaxLength(255)]
        [Description("租户编号")]
        [Required]
        public string TenantId { get; set; }
    }
}