using System;
using System.Collections.Generic;
using System.Text;
using wangwelong.BLL.Interfaces;
using wangwelong.Core.Attributes;
using wangwenlong.DAL.Interfaces;

namespace wangwelong.BLL.Services
{
    /// <summary>
    /// 方法注入
    /// </summary>
    public class Game3 : IGame
    {
        IUserInfo user = null;


        [IocMethodInjection]
        public void Init(IUserInfo _user)
        {
            user = _user;
        }


        public string PlayGame()
        {
            return $"{user.GetUserName()} 正在打游戏";
        }
    }
}
