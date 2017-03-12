/* 
 * FileName:    DefaultDataConverter.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/3/11 15:48:01
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
    public class DefaultDataConverter : IDataConverter
    {
        public static readonly DefaultDataConverter Instance = new DefaultDataConverter();

        private DefaultDataConverter() { }

        public object ConvertData(object value, Type targetType)
        {
            /*
             * 此实现作为缺省实现存在。
             * 仅支持比较基础的值类型(以及对应的Nullable类型)和string类型，使用Convert类进行转换。
             * 对于Convert不支持的转换或复杂的类型转化，需自行创建新的转换器类型并进行配置。
             */
            var underType = Nullable.GetUnderlyingType(targetType);
            if (underType != null && value == null)
            {
                return null;
            }
            var typeCode = Type.GetTypeCode(underType ?? targetType);

            switch (typeCode)
            {
                case TypeCode.Int16:
                    return Convert.ToInt16(value);
                case TypeCode.Int32:
                    return Convert.ToInt32(value);
                case TypeCode.Int64:
                    return Convert.ToInt64(value);
                case TypeCode.Byte:
                    return Convert.ToByte(value);
                case TypeCode.UInt16:
                    return Convert.ToUInt16(value);
                case TypeCode.UInt32:
                    return Convert.ToUInt32(value);
                case TypeCode.UInt64:
                    return Convert.ToUInt64(value);
                case TypeCode.SByte:
                    return Convert.ToSByte(value);
                case TypeCode.Single:
                    return Convert.ToSingle(value);
                case TypeCode.Double:
                    return Convert.ToDouble(value);
                case TypeCode.Decimal:
                    return Convert.ToDecimal(value);
                case TypeCode.Boolean:
                    return Convert.ToBoolean(value);
                case TypeCode.DateTime:
                    return Convert.ToDateTime(value);
                case TypeCode.String:
                    return Convert.ToString(value);
                case TypeCode.Char:
                    return Convert.ToChar(value);
            }
            return value;
        }
    }
}
