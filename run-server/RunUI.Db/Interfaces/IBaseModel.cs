using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public  interface IBaseModel :ICreateTimeEntity, IUpdateTimeEntity
    {
        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        int Flag { get; set; }
    }
}
