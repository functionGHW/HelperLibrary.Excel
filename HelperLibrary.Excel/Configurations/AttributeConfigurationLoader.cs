/* 
 * FileName:    AttributeConfigurationLoader.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/3/11 16:25:10
 * Version:     v1.0
 * Description:
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.Excel.Configurations
{
    /// <summary>
    /// 基于Attribute的配置加载器
    /// </summary>
    public class AttributeConfigurationLoader : IConfigurationLoader
    {
        public ModelConfiguration Load(Type modelType)
        {
            if (modelType == null)
                throw new ArgumentNullException(nameof(modelType));

            return CreateModelConfig(modelType);

        }

        private ModelConfiguration CreateModelConfig(Type modelType)
        {
            var sheetAttr = Attribute.GetCustomAttribute(modelType, typeof(SheetMappingAttribute)) as SheetMappingAttribute;
            if (sheetAttr == null)
                throw new Exception();

            var modelConfig = new ModelConfiguration()
            {
                Class = modelType.AssemblyQualifiedName,
                Sheet = string.IsNullOrEmpty(sheetAttr.Sheet) ? modelType.Name : sheetAttr.Sheet,
                StartRow = sheetAttr.StartRow,
                Properties = CreatePropertyConfig(modelType),
            };
            // 未指定Sheet属性，则默认类型名称为工作表名称
            if (string.IsNullOrEmpty(modelConfig.Sheet))
                modelConfig.Sheet = modelType.Name;

            return modelConfig;
        }

        private List<PropertyConfiguration> CreatePropertyConfig(Type modelType)
        {
            var list = new List<PropertyConfiguration>();

            var props = modelType.GetProperties();

            foreach (var prop in props)
            {
                var propConfig = CreateOnePropConfig(prop);
                if (propConfig != null)
                {
                    list.Add(propConfig);
                }
            }
            return list;
        }

        private PropertyConfiguration CreateOnePropConfig(PropertyInfo prop)
        {
            var colAttr = prop.GetCustomAttribute<ColumnMappingAttribute>();
            if (colAttr == null)
                return null;

            var result = new PropertyConfiguration()
            {
                Property = prop.Name,
                Column = colAttr.Column,
                ColumnIndex = colAttr.ColumnIndex,
                Converter = colAttr.Converter == null ? null : colAttr.Converter.AssemblyQualifiedName,
            };
            // 未指定Column和ColumnIndex配置，则默认使用属性名称作为列名
            if (result.Column == null && result.ColumnIndex < 0)
                result.Column = prop.Name;
            
            return result;
        }
    }
}
