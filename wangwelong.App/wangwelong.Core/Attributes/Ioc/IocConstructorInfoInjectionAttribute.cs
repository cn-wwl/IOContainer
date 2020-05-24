using System;
using System.Collections.Generic;
using System.Text;

namespace wangwelong.Core.Attributes
{
    /// <summary>
    /// 标记IOC需要注入的构造函数
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor)]
    public class IocConstructorInfoInjectionAttribute:Attribute
    {

    }
}
