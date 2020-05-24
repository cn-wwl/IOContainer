# IOContainer
IOC容器dotnet版



### 一、注册类型

* 接口注入
* 抽象注入
* 单接口多实现(注册时，传入特定的名字做区分即可)



### 二、注入方式

* 构造函数注入 (标记`[IocConstructorInfoInjection]`特性，指定构造函数注入)
* 属性注入 (标记`[IocPropertyInjection]`特性，指定属性注入)
* 方法注入 (标记`[IocMethodInjection]`特性，指定方法注入)



### 三、生命周期管理

* 注册瞬时类型组件 `RegisterTransient<T, U>(string shortName = null)`
* 注册单例类型组件 `RegisterSingleton<T, U>(string shortName = null)`
* 注册作用域类型组件 `RegisterScoped<T, U>(string shortName = null)`







