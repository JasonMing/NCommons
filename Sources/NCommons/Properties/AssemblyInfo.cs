using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的常规信息通过以下
// 特性集控制。更改这些特性值可修改
// 与程序集关联的信息。
#if NETFX4
[assembly: AssemblyTitle("NCommons.NET")]
[assembly: AssemblyDescription("The commons utilities for .NET 4")]
[assembly: AssemblyProduct("NCommons.NET")]
#elif WINRT81
[assembly: AssemblyTitle("NCommons.WinRT")]
[assembly: AssemblyDescription("The commons utilities for WinRT 8.1")]
[assembly: AssemblyProduct("NCommons.WinRT")]
#else
#error Not supported or undefined platform.
#endif
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyCopyright("Copyright ©  MiNG 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。  如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("bc4f113e-4199-4606-8bf1-aa8ab92c775c")]

// 程序集的版本信息由下面四个值组成: 
//
//      主版本
//      次版本 
//      生成号
//      修订号
//
// 可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值，
// 方法是按如下所示使用“*”: 
// [assembly: AssemblyVersion("1.0.*")]
#if NETFX4
[assembly: AssemblyVersion("1.0.0.4")]
[assembly: AssemblyFileVersion("1.0.0.4")]
#elif WINRT81
[assembly: AssemblyVersion("1.0.0.81")]
[assembly: AssemblyFileVersion("1.0.0.81")]
#else
#error Not supported or undefined platform.
#endif

#if NETFX4
[assembly: InternalsVisibleTo("NCommons.Tests")]
#elif WINRT81
//TODO: [assembly: InternalsVisibleTo("NCommons.Tests")]
#endif
