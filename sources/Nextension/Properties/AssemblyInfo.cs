using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的常规信息通过以下
// 特性集控制。更改这些特性值可修改
// 与程序集关联的信息。
#if NETFX45
[assembly: AssemblyTitle("Nextension")]
[assembly: AssemblyDescription("The useful extensions for .NET 4.5")]
[assembly: AssemblyProduct("Nextension")]
#elif WINRT81
[assembly: AssemblyTitle("NCommons.WinRT")]
[assembly: AssemblyDescription("The commons utilities for WinRT 8.1")]
[assembly: AssemblyProduct("NCommons.WinRT")]
#else
#error Not supported or undefined platform.
#endif
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Nextension Project")]
[assembly: AssemblyCopyright("Copyright © MiNG 2015")]
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
#if NETFX45
[assembly: AssemblyVersion("1.0.0")]
[assembly: AssemblyFileVersion("1.0.0")]
#elif WINRT81
[assembly: AssemblyVersion("1.0.0.81")]
[assembly: AssemblyFileVersion("1.0.0.81")]
#else
#error Not supported or undefined platform.
#endif

#if NETFX45
[assembly: InternalsVisibleTo("Nextension.Tests, PublicKey=002400000480000094000000060200000024000052534131000400000100010059447075141c25a123d2b3c6ac7c23a0c8ba53aca7b675643ee425d403a4db96cc819339eb6a58d6fa737792c4ed1cc65a3fb9807819f69d40a0a936db1ef7f301d2f048bb0315cf2be48e52338a4dce39d4de46bec004c7b8eb7dfa5b3205e3e6da6fa7ecf12ce3e4b1935cf7c6e43e2dd4b1668e8b34ec18565f5677c283e3")]
#elif WINRT81
//TODO: [assembly: InternalsVisibleTo("NCommons.Tests")]
#endif
