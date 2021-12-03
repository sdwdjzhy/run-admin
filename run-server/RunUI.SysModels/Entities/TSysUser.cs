using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
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

        [Column(ServerTime = DateTimeKind.Utc, CanUpdate = false)]
        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        public int TenantId { get; set; }
    }
}
