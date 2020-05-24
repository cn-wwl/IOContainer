using System;
using System.Collections.Generic;
using System.Text;

namespace wangwenlong.DAL.Services
{
    public class UserInfo2:AUserInfo
    {
        private string Name { get; set; }

        public UserInfo2()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "鲨鱼辣椒";
            }
        }
        //public UserInfo2(string username)
        //{
        //    Name = username;
        //}

        public override string GetUserName()
        {
            return Name;
        }
    }
}
