using System;
using System.Collections.Generic;
using System.Text;

namespace wangwelong.Core.Attributes
{

    /// <summary>
    /// 标记IOC需要注入的方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class IocMethodInjectionAttribute:Attribute
    {

    }
}
