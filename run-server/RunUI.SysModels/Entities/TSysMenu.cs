using FreeSql.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RunUI.SysModels
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Table(Name = "sys_menu")]
    public class TSysMenu : BaseTenantModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(255)]
        [Required]
        [Column(IsNullable = false)]
        [Description("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 辅助说明
        /// </summary>
        [Description("辅助说明")]
        [StringLength(500, MinimumLength = 0)]
        public string Description { get; set; }

        /// <summary>
        /// 菜单图标class
        /// </summary>
        [MaxLength(50)]
        [Description("菜单图标class")]
        public string IconClass { get; set; }

        /// <summary>
        /// 未激活时的自定义菜单图标图片
        /// </summary>
        [MaxLength(500)]
        [Description("未激活时的自定义菜单图标图片")]
        public string IconImageNotActive { get; set; }

        /// <summary>
        /// 激活时的自定义菜单图标图片
        /// </summary>
        [MaxLength(500)]
        [Description("激活时的自定义菜单图标图片")]
        public string IconImageActive { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        [MaxLength(50)]
        [Description("父级")]
        public string ParentId { get; set; }

        /// <summary>
        /// 同级别中的排名
        /// </summary>
        [Description("同级别中的排名")]
        public int SortId { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [Description("路由地址")]
        [MaxLength(500)]
        public string Path { get; set; }

        /// <summary>
        /// 页面的地址
        /// </summary>
        [Description("页面的地址")]
        [MaxLength(500)]
        public string RouteUrl { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        [Description("是否隐藏。选中后，本菜单只与权限有关")]
        public bool IsHide { get; set; }
    }
}