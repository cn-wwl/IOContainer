using System;
using System.Collections.Generic;
using System.Text;
using wangwelong.Core.Enums;

namespace wangwelong.Core.Models
{
    internal class UnityRegistModel
    {
        public Type TargetType { get; set; }

        public LiftTimeEnum LiftTime { get; set; }


        /// <summary>
        /// 仅限单例
        /// </summary>
        public object SingletonInstance { get; set; }
    }
}
