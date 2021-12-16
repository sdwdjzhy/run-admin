using FreeSql.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RunUI
{
    public abstract class BaseModel : IBaseModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Column(IsPrimary = true, DbType = "varchar(50) NOT NULL")]
        [Required]
        [Description("编号")]
        public string Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        [Description("状态")]
        public int Flag { get; set; }

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
        /// 最后一次操作用户编号
        /// </summary>
        [Column(DbType = "varchar(50) NOT NULL")]
        [Required]
        [Description("最后一次操作用户编号")]
        public string OperateUserId { get; set; }
    }
}