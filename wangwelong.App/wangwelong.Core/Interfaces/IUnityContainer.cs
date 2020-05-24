using System;
using System.Collections.Generic;
using System.Text;

namespace wangwelong.Core.Interfaces
{
    public interface IUnityContainer
    {
        /// <summary>
        /// 注册组件(生命周期为瞬时)
        /// </summary>
        /// <typeparam name="T">抽象类、接口</typeparam>
        /// <typeparam name="U">派生类</typeparam>
        /// <param name="shortName">派生类名称(用于单接口多实现)</param>
        void RegisterTransient<T, U>(string shortName = null) where T : class where U : T;



        /// <summary>
        /// 注册组件(生命周期为单例)
        /// </summary>
        /// <typeparam name="T">抽象类、接口</typeparam>
        /// <typeparam name="U">派生类</typeparam>
        /// <param name="shortName">派生类名称(用于单接口多实现)</param>
        void RegisterSingleton<T, U>(string shortName = null) where T : class where U : T;



        /// <summary>
        /// 注册组件(生命周期为作用域，每个HTTP请求都创建一个子容器，保证每个请求的IOC都是自己的)
        /// </summary>
        /// <typeparam name="T">抽象类、接口</typeparam>
        /// <typeparam name="U">派生类</typeparam>
        /// <param name="shortName">派生类名称(用于单接口多实现)</param>
        void RegisterScoped<T, U>(string shortName = null) where T : class where U : T;

        /// <summary>
        /// 构建子容器
        /// </summary>
        /// <returns></returns>
        IUnityContainer BuildChidContainer();

        /// <summary>
        /// 获取具体对象
        /// </summary>
        /// <typeparam name="T">抽象类、接口</typeparam>
        /// <param name="shortName">派生类名称(用于单接口多实现)</param>
        /// <returns></returns>
        T Resolve<T>(string shortName = null); 

        /// <summary>
        /// 清除组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Clear<T>();
    }
}
