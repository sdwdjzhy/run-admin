using System.ComponentModel.DataAnnotations;

namespace RunUI
{
    public class Tenant
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [Display(Name = "租户编号")]
        public string Id { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        [Display(Name = "租户名称")]
        public string Name { get; set; }

        /// <summary>
        /// 租户时长
        /// </summary>
        [Display(Name = "租户时长")]
        public DateTime? Expire { get; set; }
    }
}