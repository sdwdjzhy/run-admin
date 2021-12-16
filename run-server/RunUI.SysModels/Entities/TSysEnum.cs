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
    [Table(Name = "sys_enum")]
    [Description("系统字典")]
    public class TSysEnum : BaseTenantModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(255)]
        [Required]
        [Description("字典名称")]
        public string Name { get; set; }
    }


    [Table(Name = "sys_enun_item")]
    [Description("系统字典项")]
    public class TSysEnumItem : BaseTenantModel, ISortIdEntity, IParentIdEntity
    {
        /// <summary>
        /// 字典项名称
        /// </summary>
        [MaxLength(255)]
        [Required]
        [Description("字典项名称")]
        public string Name { get; set; }

        /// <summary>
        /// 字典编号
        /// </summary>
        [MaxLength(255)]
        [Required]
        [Column(IsNullable = false)]
        [Description("字典编号")]
        public string SysEnumId { get; set; }

        /// <summary>
        /// 字典上级字典项编号
        /// </summary>
        [MaxLength(255)]
        [Description("字典上级字典项编号")]
        public string ParentId { get; set; }


        /// <summary>
        /// 字典项排序
        /// </summary>
        [MaxLength(255)]
        [Required]
        [Description("字典项排序")]
        public int SortId { get; set; }
    }
}
