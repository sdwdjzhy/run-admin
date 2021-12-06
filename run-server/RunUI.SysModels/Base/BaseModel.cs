using FreeSql.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RunUI.SysModels
{
    public abstract class BaseModel : IDeleteEntity, ICreateTimeEntity, IUpdateTimeEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Column(IsPrimary = true, DbType = "varchar(50) NOT NULL")]
        [Required]
        [Description("编号")]
        public string Id { get; set; }

        /// <summary>
        /// 是否已经删除
        /// </summary>
        [Required]
        [Description("是否已经删除")]
        public bool IsDeleted { get; set; }

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