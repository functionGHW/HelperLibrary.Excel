/* 
 * FileName:    ModelConfiguration.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/3/11 12:52:53
 * Version:     v1.0
 * Description:
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HelperLibrary.Excel.Configurations
{
    /// <summary>
    /// 表示一个Model类型的配置信息
    /// </summary>
    [XmlType("Model")]
    public class ModelConfiguration
    {
        /// <summary>
        /// Model类型名称，包含命名空间。如果类型不在当前程序集中，需要加入程序集名称限定。
        /// </summary>
        [XmlAttribute]
        public string Class { get; set; }


        /// <summary>
        /// （可选）配置标签，同一个类型可以存在多个不同标签的配置
        /// </summary>
        [XmlAttribute]
        public string Tag { get; set; }

        /// <summary>
        /// Excel工作表名称。
        /// </summary>
        [XmlAttribute]
        public string Sheet { get; set; }

        /// <summary>
        /// 表格起始行行号(从1开始)，在此之前的行将被跳过。
        /// </summary>
        [XmlAttribute]
        public int StartRow { get; set; }

        /// <summary>
        /// 表格表头所在行行号(从1开始)，如果指定小于零的值且NoHeaderRow为False，则默认StartRow上一行为表头行
        /// </summary>
        [XmlAttribute]
        public int HeaderRow { get; set; }

        /// <summary>
        /// 如果为True，则表明表格没有表头行，将忽略相关配置；此时无法通过列名自动获取列索引，必须手动指定列索引。
        /// </summary>
        [XmlAttribute]
        public bool NoHeaderRow { get; set; }

        /// <summary>
        /// 是否跳过空白行
        /// </summary>
        [XmlAttribute]
        public bool SkipEmptyRow { get; set; }

        /// <summary>
        /// 属性映射配置
        /// </summary>
        [XmlElement("Property")]
        public List<PropertyConfiguration> Properties;
    }
}
