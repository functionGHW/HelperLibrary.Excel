/* 
 * FileName:    IDataConverter.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/3/11 15:47:08
 * Version:     v1.0
 * Description:
 * */

using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.Excel
{
    /// <summary>
    /// 数据转换类型接口
    /// </summary>
    public interface IDataConverter
    {
        object ConvertData(object value, Type targetType);
    }
}
