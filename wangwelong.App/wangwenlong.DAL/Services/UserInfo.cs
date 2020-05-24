using System;
using System.Collections.Generic;
using System.Text;
using wangwenlong.DAL.Interfaces;

namespace wangwenlong.DAL.Services
{
    public class UserInfo:IUserInfo
    {
        private string Name { get; set; }  

        public UserInfo()
        { 
            if (string.IsNullOrEmpty(Name))
            {
                Name = "铁甲小宝";
            }
        }
        //public UserInfo(string username)
        //{
        //    Name = username; 
        //}

        public string GetUserName()
        {
            return Name;  
        }
    }
}
