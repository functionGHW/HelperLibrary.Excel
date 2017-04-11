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

        /// <summary>
        /// 表格表头所在行行号(从1开始)，如果指定小于零的值且NoHeaderRow为False，则默认StartRow上一行为表头行
        /// </summary>
        public int HeaderRow { get; set; }

        /// <summary>
        /// 如果为True，则表明表格没有表头行，将忽略相关配置；此时无法通过列名自动获取列索引，必须手动指定列索引。
        /// </summary>
        public bool NoHeaderRow { get; set; }

        /// <summary>
        /// 是否跳过空白行
        /// </summary>
        public bool SkipEmptyRow { get; set; }
    }
}
