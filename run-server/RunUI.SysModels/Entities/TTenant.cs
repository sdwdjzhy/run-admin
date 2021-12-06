using FreeSql.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RunUI.SysModels
{
    [Table(Name = "sys_tenant")]
    [Description("租户")]
    public class TTenant : BaseModel
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [Description("租户名称")]
        [MaxLength(255)]
        [Required]
        [Column(IsNullable = false)]
        public string Name { get; set; }
    }
}