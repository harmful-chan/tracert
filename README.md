# tracert

## 说明
+ 基于.NET Core 3.0开发
+ 为 Span 项目提供路由跟踪服务，可获取特定的公网IP发送并记录

## 使用方法
+ 路由跟踪
```
>tracert www.baidu.com
1 192.168.1.1
2 10.2.199.254
3 172.16.10.29
4 *
5 172.23.252.105
6 183.36.223.17
7 183.56.34.245
8 *
9 113.96.5.46
10 *
11 14.29.117.242
12 14.215.177.38
```
+ 获取特定地址
```
>tracert www.baidu.com 3
 172.16.10.29
```

## 其他
+ runtime 下载地址：
+ [.net core runtime for window x64](https://download.visualstudio.microsoft.com/download/pr/f15b7c04-2900-4a14-9c01-ccd66a4323cc/17a6bbd44f0d0a85d219dd9e166a89ca/dotnet-runtime-3.0.0-win-x64.zip)
+ [.net core runtime for linux x64](https://download.visualstudio.microsoft.com/download/pr/a5ff9cbb-d558-49d1-9fd2-410cb1c8b095/a940644f4133b81446cb3733a620983a/dotnet-runtime-3.0.0-linux-x64.tar.gz)
