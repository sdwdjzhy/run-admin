using FreeSql.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RunUI.SysModels
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Table(Name = "sys_test")]
    public class TSysTest : BaseTenantModel
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