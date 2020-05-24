using System;
using System.Collections.Generic;
using System.Text;
using wangwelong.BLL.Interfaces;
using wangwenlong.DAL;
using wangwenlong.DAL.Interfaces; 
using wangwelong.Core.Attributes;

namespace wangwelong.BLL.Services
{
    /// <summary>
    /// 构造函数注入
    /// </summary>
    public class Game:IGame
    {
        public string GameName { get; set; }
        public string userName { get; set; }
        public string auserName { get; set; }

        public Game(IUserInfo user)
        {
            userName = user.GetUserName();
        } 

        [IocConstructorInfoInjection] //标记要注入的构造方法，不加标记默认注入参数最多的构造函数
        public Game(IUserInfo user, AUserInfo aUser)
        {
            userName = user.GetUserName();
            auserName = aUser.GetUserName();
        }

        public Game(IUserInfo user, AUserInfo aUser,string gameName)
        {
            GameName = gameName;
            userName = user.GetUserName();
            auserName = aUser.GetUserName();
        }

        public string PlayGame()
        {
            return $"{userName} 正在打游戏,{auserName}在旁边观看";
        }

    }
}
