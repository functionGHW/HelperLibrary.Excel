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
        /// 属性映射配置
        /// </summary>
        [XmlElement("Property")]
        public List<PropertyConfiguration> Properties;
    }
}
