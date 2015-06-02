# pomelo-dotnetClient
pomelo .net客户端修改版，同时可以在unity3D 5 中使用。该修改主要是给socket的Connect方法加了个异步，不然会阻塞主线程的

原始库来自：https://github.com/koalaylj/pomelo-client-proto-test

该客户端基于其中的pomelo-dotnetClient类库修改而来
我主要是为了方便在 unity3D 上的使用

主要改进了：
1，用异步解决了 socket.Connect 调用时造成的主线程阻塞问题
2，增加了3个新的 event 委托
①	OnConnect =  连接成功
②	OnDisconnect =  中断连接
③	OnException =  连接异常

注：项目内部自带一个简单的 dotnet 窗口程序作为例子，也可以进行简单的连接测试。
