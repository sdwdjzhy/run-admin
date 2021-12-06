using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
