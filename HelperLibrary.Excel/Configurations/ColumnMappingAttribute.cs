/* 
 * FileName:    ColumnMappingAttribute.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/3/11 16:15:43
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
    /// 表格列映射信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnMappingAttribute : Attribute
    {
        public ColumnMappingAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column">映射的列名</param>
        public ColumnMappingAttribute(string column)
        {
            Column = column;
        }

        /// <summary>
        /// 映射的列索引
        /// </summary>
        public int ColumnIndex { get; set; } = -1;

        /// <summary>
        /// 映射的列名
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// 数据转化器类型
        /// </summary>
        public Type Converter { get; set; }
    }
}
