using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ForeignEnumAttribute : Attribute
    {
        public string EnumCode { get; set; }
        public ForeignEnumAttribute(string enumCode)
        {
            EnumCode = enumCode;
        }
    }
}
