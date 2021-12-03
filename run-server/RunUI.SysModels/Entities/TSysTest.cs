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
    [Table(Name = "sys_test")]
    public class TSysTest
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Column(IsPrimary = true, DbType = "varchar(50) NOT NULL")]
        [Required]
        [Description("编号")]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(255)]
        [Required]
        [Column(IsNullable = false)]
        [Description("名称")]
        public string Name { get; set; }

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

        /// <summary>
        /// 租户编号
        /// </summary>
        [Required]
        [Description("租户编号")]
        public int TenantId { get; set; }


        /// <summary>
        /// 金额
        /// </summary> 
        [Description("金额")]
        [Column(DbType = "decimal(18, 4)")]
        public decimal? Money { get; set; }


        /// <summary>
        /// 价格
        /// </summary> 
        [Description("价格")]
        [Column(DbType = "decimal(18, 4)")]
        public decimal Price { get; set; }



        /// <summary>
        /// 经度
        /// </summary> 
        [Description("经度")]
        [Column(DbType = "decimal(18, 15)")]
        public decimal? Lng { get; set; }


        /// <summary>
        /// 纬度
        /// </summary> 
        [Description("纬度")]
        [Column(DbType = "decimal(18, 15)")]
        public decimal? Lat { get; set; }


        /// <summary>
        /// 是否激活
        /// </summary> 
        [Description("是否激活")]
        public bool IsUsed { get; set; }


        /// <summary>
        /// 性别
        /// </summary> 
        [Description("性别")]
        public AppEnum.Sex Sex { get; set; }



        /// <summary>
        /// Html内容
        /// </summary> 
        [Description("Html内容")]
        [Column(DbType = "varchar(5000)")]
        public string HtmlContent { get; set; }
    }
}
