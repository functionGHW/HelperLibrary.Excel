/* 
 * FileName:    IConfigurationLoader.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/3/11 13:50:18
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
    /// 类型映射配置的加载器接口
    /// </summary>
    public interface IConfigurationLoader
    {
        /// <summary>
        /// 加载指定类型的映射配置
        /// </summary>
        /// <param name="modelType">指定的类型</param>
        /// <param name="tag">可选的唯一标签值</param>
        /// <returns>指定类型映射配置的实例，如果不存在则返回null</returns>
        ModelConfiguration Load(Type modelType, string tag = null);
    }
}
