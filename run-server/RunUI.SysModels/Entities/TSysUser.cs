using FreeSql.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RunUI.SysModels
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Table(Name = "sys_user")]
    public class TSysUser : BaseTenantModel
    {
        /// <summary>
        /// 登录用户名
        /// </summary>
        [MaxLength(255)]
        [Required]
        [Column(IsNullable = false)]
        [Description("登录用户名")]
        public string Name { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [MaxLength(255)]
        [Required]
        [Column(IsNullable = false)]
        [Description("登录密码")]
        public string Password { get; set; }
    }
}