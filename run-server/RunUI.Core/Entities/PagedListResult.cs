using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public class PagedListResult<T>
    {
        public PagedList<T> List { get; set; }
        public List<LabelValueGroup> Select { get; set; }
    }
}
