using System;
using wangwelong.Core.Interfaces;
using wangwelong.Core.Container;
using wangwenlong.DAL.Interfaces;
using wangwenlong.DAL.Services;
using wangwelong.BLL.Interfaces;
using wangwelong.BLL.Services;
using wangwenlong.DAL;

namespace wangwelong.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 注册方式(接口注册、抽象注册)

            //IUnityContainer container = new UnityContainer();

            //1.接口注册
            //container.RegisterTransient<IUserInfo, UserInfo>();
            //IUserInfo userInfo = container.Resolve<IUserInfo>();
            //var name = userInfo.GetUserName();
            //Console.WriteLine($"接口注册UserInfo:{name}");


            //2.抽象注册
            //container.RegisterTransient<AUserInfo, UserInfo2>();
            //AUserInfo userInfo2 = container.Resolve<AUserInfo>();
            //var name2 = userInfo2.GetUserName();
            //Console.WriteLine($"抽象注册UserInfo:{name2}");

            #endregion

            #region 注入方式(构造函数注入、属性注入、方法注入)
            //IUnityContainer container = new UnityContainer();

            //1.构造函数注入
            //container.RegisterTransient<IGame, Game>("game");
            //var game = container.Resolve<IGame>("game");
            //var playname = game.PlayGame();
            //Console.WriteLine($"构造函数注入Game:{playname}");


            //2.属性注入
            //container.RegisterTransient<IGame, Game2>("game2");
            //var game1 = container.Resolve<IGame>("game2");
            //var playname1 = game1.PlayGame();
            //Console.WriteLine($"属性注入Game2:{playname1}");

            //3.方法注入
            //container.RegisterTransient<IGame, Game3>("game3");
            //var game2 = container.Resolve<IGame>("game3");
            //var playname2 = game2.PlayGame();
            //Console.WriteLine($"方法注入Game3:{playname2}");

            #endregion

            #region 生命周期管理(瞬时、单例、作用域)

            //1.瞬时(上面的注册和注入方式中的方式的生命周期即是瞬时形态，每次注册都是新的实例)


            //2.单例(保证注册一次后只创建一个实例，每次在获取对象的时候都是同一个实例)
            //IUnityContainer container = new UnityContainer();
            //container.RegisterSingleton<IUserInfo, UserInfo>();
            //IUserInfo user1 = container.Resolve<IUserInfo>();
            //IUserInfo user2 = container.Resolve<IUserInfo>();
            //Console.WriteLine(object.ReferenceEquals(user1,user2));//T 返回结果为Ture就是同一个实例

            //3.作用域(就是Http请求时，一个请求处理过程中，创建都是同一个实例；不同的请求处理过程中，就是不同的实例)
            //需要区分请求，Http请求——Asp.NetCore内置Kestrel,初始化一个容器实例，每次连接一个新的http请求，都会创建一个子容器

            IUnityContainer container = new UnityContainer();
            container.RegisterScoped<IUserInfo, UserInfo>();  

            IUnityContainer container1= container.BuildChidContainer();
            IUserInfo user11 = container1.Resolve<IUserInfo>();
            IUserInfo user12 = container1.Resolve<IUserInfo>();
             
            IUnityContainer container2 = container.BuildChidContainer();
            IUserInfo user21 = container2.Resolve<IUserInfo>();
            IUserInfo user22 = container2.Resolve<IUserInfo>();


            Console.WriteLine(object.ReferenceEquals(user11, user12));//T 同一个子容器创建的都是同一个实例

            Console.WriteLine(object.ReferenceEquals(user21, user22));//T 同一个子容器创建的都是同一个实例

            Console.WriteLine(object.ReferenceEquals(user11, user21));//F 不同子容器创建的不是同一个实例
            Console.WriteLine(object.ReferenceEquals(user11, user22));//F 不同子容器创建的不是同一个实例
            Console.WriteLine(object.ReferenceEquals(user12, user21));//F 不同子容器创建的不是同一个实例
            Console.WriteLine(object.ReferenceEquals(user12, user22));//F 不同子容器创建的不是同一个实例


            #endregion


            Console.ReadKey();
        }
    }
}
