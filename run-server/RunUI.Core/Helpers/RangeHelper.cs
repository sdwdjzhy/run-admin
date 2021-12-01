using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public class RangeHelper
    {
        public IEnumerable<int> Range(int start, int length)
        {
            return Enumerable.Range(start, length);
        }
    }
}
