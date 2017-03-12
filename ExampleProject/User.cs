/* 
 * FileName:    User.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/3/11 15:23:18
 * Version:     v1.0
 * Description:
 * */

using HelperLibrary.Excel.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProject
{
    [SheetMapping("用户数据")]
    public class User
    {
        [ColumnMapping("编号")]
        public int? ID { get; set; }

        [ColumnMapping("姓名")]
        public string Name { get; set; }

        [ColumnMapping()]
        public int Age { get; set; }

        [ColumnMapping("邮箱")]
        public string Email { get; set; }

        [ColumnMapping("生日")]
        public DateTime? Birthday{ get; set; }
    }
}
