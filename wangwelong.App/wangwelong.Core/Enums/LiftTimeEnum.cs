using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace wangwelong.Core.Enums
{
    internal enum LiftTimeEnum
    {
        [Description("瞬时")]
        Transient,
        [Description("单例")]
        Singleton,
        [Description("作用域")]
        Scoped,
    }
}
