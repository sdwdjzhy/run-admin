using FreeSql.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RunUI.SysModels
{ 
    public abstract class BaseTenantModel : BaseModel, ITenantEntity
    {

        /// <summary>
        /// 租户编号
        /// </summary>
        [Description("租户编号")]
        [Required]
        public string TenantId { get; set; }
    }
}
