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
    /// <summary>
    /// 系统用户
    /// </summary>
    [Table(Name ="sys_user")]
    public class TSysUser
    {
        [Column(IsPrimary = true, DbType = "varchar(50) NOT NULL")]
        [Required]
        public string Id { get; set; }

        [MaxLength(255)]
        [Required]
        [Column(IsNullable = false)]
        public string Name { get; set; }

        [MaxLength(255)]
        [Required]
        [Column(IsNullable = false)]
        public string Password { get; set; } 

        [Required]
        public int TenantId { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Utc, CanUpdate = false)]
        [Required]
        [Description("创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Utc)]
        [Required]
        [Description("更新时间")]
        public DateTime UpdateTime { get; set; }
    }
}
