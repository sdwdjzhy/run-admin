using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public class PagedList<T>
    {
        /// <summary>
        /// 总共数量
        /// </summary>
        public long Total { get; set; }
        /// <summary>
        /// 项列表
        /// </summary>
        public List<T> Rows { get; set; }
        /// <summary>
        /// 当前页，从1开始
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }

    }
}
