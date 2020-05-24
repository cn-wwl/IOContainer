using System;
using System.Collections.Generic;
using System.Text;
using wangwelong.BLL.Interfaces;
using wangwelong.Core.Attributes;
using wangwenlong.DAL.Interfaces;

namespace wangwelong.BLL.Services
{

    /// <summary>
    /// 属性注入
    /// </summary>
    public class Game2:IGame
    { 
        [IocPropertyInjection]
        public IUserInfo user { get; set; }

        public string PlayGame()
        {
            return $"{user.GetUserName()} 正在打游戏";
        }
    }
}
