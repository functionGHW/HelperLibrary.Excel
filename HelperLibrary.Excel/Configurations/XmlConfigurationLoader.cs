﻿/* 
 * FileName:    XmlConfigurationLoader.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/3/11 13:49:04
 * Version:     v1.0
 * Description:
 * */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HelperLibrary.Excel.Configurations
{
    /// <summary>
    /// 基于Xml文件的配置加载器
    /// </summary>
    public class XmlConfigurationLoader : IConfigurationLoader
    {

        private string filePath;
        private Lazy<List<ModelConfiguration>> configsLazy;

        private static readonly Lazy<XmlSerializer> serializerLazy = new Lazy<XmlSerializer>(
            () => new XmlSerializer(typeof(List<ModelConfiguration>), new XmlRootAttribute("Configurations")));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public XmlConfigurationLoader(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            this.filePath = filePath;

            configsLazy = new Lazy<List<ModelConfiguration>>(ReadAllConfigs);
        }

        private List<ModelConfiguration> ReadAllConfigs()
        {
            var serializer = serializerLazy.Value;
            using (var fs = File.OpenRead(filePath))
            {
                List<ModelConfiguration> result = null;
                try
                {
                    result = serializer.Deserialize(fs) as List<ModelConfiguration>;
                }
                catch (Exception ex)
                {
                    throw new DataReaderException("Loading configurations failed.", ex);
                }
                if (result == null)
                    throw new DataReaderException("Loading configurations failed. File path is " + filePath);

                return result;
            }
        }

        public ModelConfiguration Load(Type modelType)
        {
            var allConfigs = configsLazy.Value;

            var result = allConfigs.FirstOrDefault(item => item.Class == modelType.AssemblyQualifiedName
                                            || item.Class == modelType.FullName);

            // 检查是否有配置缺失
            foreach (var p in result.Properties)
            {
                if (string.IsNullOrEmpty(p.Property))
                    throw new DataReaderException($"Configuration error: Property is required. Type is {result.Class}.");

                if (string.IsNullOrEmpty(p.Column) && p.ColumnIndex < 0)
                    throw new DataReaderException($"Configuration error: either Column or ColumnIndex is required for property {p.Property}. Type is {result.Class}.");
            }
            return result;
        }
    }
}
