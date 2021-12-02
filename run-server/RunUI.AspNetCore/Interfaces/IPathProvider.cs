using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI 
{
    public interface IPathProvider
    {
        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string MapPath(string path);
    }
}
