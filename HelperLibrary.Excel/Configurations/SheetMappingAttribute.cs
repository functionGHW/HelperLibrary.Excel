/* 
 * FileName:    SheetMappingAttribute.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/3/11 16:16:34
 * Version:     v1.0
 * Description:
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.Excel.Configurations
{
    /// <summary>
    /// Excel工作表映射配置信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SheetMappingAttribute : Attribute
    {
        public SheetMappingAttribute() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet">映射的工作表名称</param>
        public SheetMappingAttribute(string sheet)
        {
            Sheet = sheet;
        }

        /// <summary>
        /// 映射的工作表名称
        /// </summary>
        public string Sheet { get; set; }

        /// <summary>
        /// 工作表起始行行号
        /// </summary>
        public int StartRow { get; set; }
    }
}
