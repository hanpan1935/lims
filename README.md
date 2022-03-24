# lims

### 一:介绍
实验室信息管理系统

### 二:软件架构
.net6.0 (webapi+wpf) 前后分离


### 三:系统要求
1. windows 7+

2. .net6 SKD
下载地址: https://dotnet.microsoft.com/zh-cn/download


3. sql server 2014+
下载地址: https://www.microsoft.com/zh-cn/sql-server/sql-server-downloads

默认数据库链接字符串: "Default": "Server=.\\SQLEXPRESS;Database=Lims;Trusted_Connection=True"

### 四:如何下载

1.  下载安装包

选择最新版本进行下载,下载过程如下图

![avatar](https://github.com/hanpan1935/lims/blob/main/document/image/download/1.png)

![avatar](https://github.com/hanpan1935/lims/blob/main/document/image/download/2.png)



2.  解压安装包


![avatar](https://github.com/hanpan1935/lims/blob/main/document/image/download/3.png)

其中
Server 为服务端程序
Client 为客户端程序
DbMigrator 为数据库迁移程序



### 五:如何使用


####  如何运行

1 运行数据库迁移程序
注意:只再首次运行或者版本更新的的的时候运行数据库迁移程序,用来生成默认数据库,平时不需要运行(日常使用只需要运行Server和Client)

![avatar](https://github.com/hanpan1935/lims/blob/main/document/image/installApp/1.png)

打开 DbMigrator/net6.0
双击运行 Lanpuda.Lims.DbMigrator.exe

2 运行服务端程序 
双击运行 Lanpuda.Lims.HttpApi.Host.exe

![avatar](https://github.com/hanpan1935/lims/blob/main/document/image/installApp/2.png)


注意:
如果出现点击后无反应的情况,请先安装.Net6 SDK

如果运行成功,打开浏览器输入  http://localhost:5000  会显示如下图所示页面

![avatar](https://github.com/hanpan1935/lims/blob/main/document/image/installApp/swger.png)



3 运行客服端
打开 Client\net6.0-windows
双击运行 Lanpuda.Lims.Client.exe

![avatar](https://github.com/hanpan1935/lims/blob/main/document/image/installApp/3.png)






### 如何更新

下载安装包 (参考如何下载)

依次运行 数据库迁移程序 ,服务端程序 ,客户端程序
