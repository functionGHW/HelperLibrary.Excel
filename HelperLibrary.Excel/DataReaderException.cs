/* 
 * FileName:    DataReaderException.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/3/14 11:01:59
 * Version:     v1.0
 * Description:
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.Excel
{
    public class DataReaderException : Exception
    {
        public DataReaderException(string message) : base(message)
        { }

        public DataReaderException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
