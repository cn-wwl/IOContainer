using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using wangwelong.Core.Interfaces;
using System.Reflection;
using wangwelong.Core.Attributes;
using wangwelong.Core.Models;
using wangwelong.Core.Enums;

namespace wangwelong.Core.Container
{
    public class UnityContainer : IUnityContainer
    {
        /// <summary>
        /// 组件集合
        /// </summary>
        private Dictionary<string, UnityRegistModel> _containerDictionary = new Dictionary<string, UnityRegistModel>();


        /// <summary>
        /// 作用域单例
        /// </summary>
        private Dictionary<string, object> _containerScopeDictionary = new Dictionary<string, object>();
         
        public IUnityContainer BuildChidContainer()
        {
            return new UnityContainer(this._containerDictionary, new Dictionary<string, object>());
        }

        public UnityContainer()
        {

        }

        private UnityContainer(Dictionary<string, UnityRegistModel> containerDictionary,Dictionary<string,object> containerScopeDictionary)
        {
            this._containerDictionary = containerDictionary;
            this._containerScopeDictionary = containerScopeDictionary;
        } 

        private string GetContainerKey(string fullName, string shortName) { return $"{fullName}___{shortName}"; }

        public void RegisterTransient<T, U>(string shortName = null) where T : class where U : T
        {
            if (!_containerDictionary.Any(a => a.Key.Equals(typeof(T).FullName)))
            {
                _containerDictionary.Add(
                    GetContainerKey(typeof(T).FullName, shortName),
                    new UnityRegistModel()
                    {
                        TargetType = typeof(U),
                        LiftTime = LiftTimeEnum.Transient
                    }
                );
            }
        }


        public void RegisterSingleton<T, U>(string shortName = null) where T : class where U : T
        {
            if (!_containerDictionary.Any(a => a.Key.Equals(typeof(T).FullName)))
            {
                _containerDictionary.Add(
                     GetContainerKey(typeof(T).FullName, shortName),
                    new UnityRegistModel()
                    {
                        TargetType = typeof(U),
                        LiftTime = LiftTimeEnum.Singleton
                    }
                );
            }
        }

        public void RegisterScoped<T, U>(string shortName = null) where T : class where U : T
        {
            if (!_containerDictionary.Any(a => a.Key.Equals(typeof(T).FullName)))
            {
                _containerDictionary.Add(
                     GetContainerKey(typeof(T).FullName, shortName),
                    new UnityRegistModel()
                    {
                        TargetType = typeof(U),
                        LiftTime = LiftTimeEnum.Scoped
                    }
                );
            }
        }
        private object ResolveObject(Type abstractType, LiftTimeEnum liftTime=LiftTimeEnum.Transient, string shortName = null)
        {             
            string key = GetContainerKey(abstractType.FullName, shortName);
            if (!_containerDictionary.ContainsKey(key))
            {
                return null;
            }
            var model = _containerDictionary[key];
             
            switch (model.LiftTime)
            {
                case LiftTimeEnum.Transient:
                    //Console.WriteLine("瞬时");
                    break;
                case LiftTimeEnum.Singleton:
                    if (model.SingletonInstance==null)
                    {
                        break;
                    }
                    else
                    {
                        return model.SingletonInstance;
                    }
                case LiftTimeEnum.Scoped:
                    if (this._containerScopeDictionary.ContainsKey(key))
                    {
                        return this._containerScopeDictionary[key];
                    }
                    else
                    {
                        break;
                    }
                default:
                    break;
            }
            Type type = model.TargetType;

            #region 构造函数注入

            ConstructorInfo ctor = null;

            //选择 IOC构造函数特性的构造函数进行注入，如果没有则注入函数最多的构造函数
            ctor = type.GetConstructors().FirstOrDefault(s => s.IsDefined(typeof(IocConstructorInfoInjectionAttribute), true));
            if (ctor == null)
            {
                ctor = type.GetConstructors().OrderByDescending(s => s.GetParameters().Length).First();
            }

            List<object> paralist = new List<object>();
            foreach (var para in ctor.GetParameters())
            {
                Type paratype = para.ParameterType;//获取参数本身的类型
                object paraInstance =this.ResolveObject(paratype);
                paralist.Add(paraInstance);
            }
            #endregion 

            object oInstance = Activator.CreateInstance(type, paralist.ToArray());

            #region 属性注入

            foreach (var prop in type.GetProperties().Where(w=> w.IsDefined(typeof(IocPropertyInjectionAttribute),true)))
            {
                Type propType = prop.PropertyType;
                object paraInstance = this.ResolveObject(propType);
                prop.SetValue(oInstance, paraInstance);
            }

            #endregion

            #region 方法注入

            foreach (var method in type.GetMethods().Where(w => w.IsDefined(typeof(IocMethodInjectionAttribute), true)))
            {
                foreach (var para in method.GetParameters())
                {
                    List<object> paramethodlist = new List<object>();
                    Type paratype = para.ParameterType;
                    object paraInstance = this.ResolveObject(paratype);
                    paramethodlist.Add(paraInstance);

                    method.Invoke(oInstance, paramethodlist.ToArray());
                }
            }

            #endregion


            #region 生命周期管理

            switch (model.LiftTime)
            {
                case LiftTimeEnum.Transient:
                    //Console.WriteLine("瞬时 after");
                    break;
                case LiftTimeEnum.Singleton: 
                    model.SingletonInstance = oInstance;
                    break;
                case LiftTimeEnum.Scoped:
                    _containerScopeDictionary[key] = oInstance;
                    break;
                default:
                    break;
            }
            #endregion

            return oInstance;
        }
        public T Resolve<T>(string shortName = null)
        {
            return (T)this.ResolveObject(typeof(T),LiftTimeEnum.Transient, shortName);
        }
      
        public void Clear<T>()
        {
            if (_containerDictionary.Any(a => a.Key.Equals(typeof(T).FullName)))
            {
                _containerDictionary.Remove(typeof(T).FullName);  
                //var lstTypes = _containerDictionary.Keys.Where(type => typeof(T).IsAssignableFrom(type)).ToList();
                //foreach (Type type in lstTypes)
                //{
                //    //typeDictionary.Remove(type);
                //    _containerDictionary.Remove(type.FullName);
                //}
            }
        }

    }
}
