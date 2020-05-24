using System;
using System.Collections.Generic;
using System.Text;

namespace wangwelong.Core.Attributes
{
    /// <summary>
    /// 标记IOC需要注入的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IocPropertyInjectionAttribute : Attribute
    {

    }
}
