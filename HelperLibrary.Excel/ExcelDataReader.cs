/* 
 * FileName:    ExcelDataReader.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/3/11 14:32:08
 * Version:     v1.0
 * Description:
 * */

using HelperLibrary.Excel.Configurations;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.Excel
{
    /// <summary>
    /// 数据读取器
    /// </summary>
    public class ExcelDataReader
    {
        private IConfigurationLoader configLoader;
        private string filePath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="configLoader">类型映射配置加载器</param>
        public ExcelDataReader(string filePath, IConfigurationLoader configLoader)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (configLoader == null)
                throw new ArgumentNullException(nameof(configLoader));

            this.filePath = filePath;
            this.configLoader = configLoader;
        }

        /// <summary>
        /// 从Excel文件中读取指定类型的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> ReadData<T>() where T : class, new()
        {
            var modelType = typeof(T);
            var configModel = configLoader.Load(modelType);

            if (configModel == null)
                // 配置不存在
                throw new DataReaderException($"Configurationo not found: type is '{modelType.AssemblyQualifiedName}'.");

            var wb = WorkbookFactory.Create(filePath);
            var sheet = wb.GetSheet(configModel.Sheet);
            int startRow = configModel.StartRow < 1 ? 1 : configModel.StartRow;
            
            if (sheet.LastRowNum < startRow)
                // 起始行号错误
                throw new DataReaderException($"StartRow configuration not correct. Type is {configModel.Class}.");

            var formatter = new DataFormatter();
            var headers = new ColumnNameIndexPair[0];
            if (!configModel.NoHeaderRow)
            {
                int headerRowNum = configModel.HeaderRow > 0 ? configModel.HeaderRow - 1 : startRow - 1;
                if (headerRowNum >= startRow)
                    // 表头行必须在起始数据行之前
                    throw new DataReaderException($"StartRow must greater than HeaderRow. Type is {configModel.Class}.");

                var headerRow = sheet.GetRow(headerRowNum);
                if (headerRow != null)
                {
                    headers = headerRow.Select(cell => new ColumnNameIndexPair
                    {
                        Column = formatter.FormatCellValue(cell),
                        Index = cell.ColumnIndex
                    }).ToArray();
                }

            }
            var validMappings = (from c in configModel.Properties
                                 let tmp = headers.FirstOrDefault(h => h.Column == c.Column)
                                 let hasColumn = !string.IsNullOrEmpty(c.Column)
                                 where modelType.GetProperty(c.Property) != null && (!hasColumn || (hasColumn && tmp != null))
                                 select new
                                 {
                                     c.Property,
                                     c.Converter,
                                     Index = string.IsNullOrEmpty(c.Column) ? c.ColumnIndex :
                                            tmp == null ? -1 : tmp.Index
                                 }).ToArray();

            bool skipEmptyRow = configModel.SkipEmptyRow;
            var list = new List<T>();
            for (int i = startRow; i < sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);

                if (row == null)
                    continue;

                if (skipEmptyRow && IsBlankRow(row))
                {
                    // 跳过空行
                    continue;
                }
                var model = new T();
                foreach (var item in validMappings)
                {
                    var prop = modelType.GetProperty(item.Property);
                    IDataConverter c = GetDataConverter(item.Converter);
                    var cell = row.GetCell(item.Index);
                    if (cell == null)
                    {
                        continue;
                    }
                    object value;
                    switch (cell.CellType)
                    {
                        case CellType.Numeric:
                            // 进一步验证是否为Date数据
                            if (DateUtil.IsCellDateFormatted(cell))
                                value = cell.DateCellValue;
                            else
                                value = cell.NumericCellValue;
                            break;
                        case CellType.String:
                            value = cell.StringCellValue;
                            break;
                        case CellType.Boolean:
                            value = cell.BooleanCellValue;
                            break;
                        default:
                            value = null;
                            break;
                    }
                    prop.SetValue(model, c == null ? value : c.ConvertData(value, prop.PropertyType));
                }
                list.Add(model);
            }
            return list;
        }

        private bool IsBlankRow(IRow row)
        {
            return row.All(cell => cell.CellType == CellType.Blank);
        }

        private IDataConverter GetDataConverter(string converter)
        {
            if (string.IsNullOrEmpty(converter))
                return DefaultDataConverter.Instance;

            var t = Type.GetType(converter);
            return Activator.CreateInstance(t) as IDataConverter;
        }

        private class ColumnNameIndexPair
        {
            public string Column { get; set; }

            public int Index { get; set; }
        }
    }
}
