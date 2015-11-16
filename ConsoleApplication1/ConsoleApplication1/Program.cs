using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
              object obj = new string[] { "172.18.105.105", "D:\\123.mp4" };
                //to parse the obj to IP and filename   
                string[] str = (string[])(obj);
                string clientIP = str[0];
                string filename = str[1];
                try
                {
                    Console.WriteLine("123");
                    //initialize a Socket Instance;    
                    Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    IPAddress hostIP = IPAddress.Parse("172.18.105.105");  //将主机名或 IP 地址解析为 IPHostEntry 实例。    
                    //将网络端点表示为 IP 地址和端口号。    
                    IPEndPoint ep = new IPEndPoint(hostIP, 10000);
                    //用指定的地址和端口号初始化 IPEndPoint 类的新实例。        
                    ////取得主机IP    
                    //IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());                
                    //IPAddress ipAddr = ipHost.AddressList[0];    
                    //IPEndPoint ep1 = new IPEndPoint(ipAddr, 11000);      
                    //用指定的地址和端口号初始化 IPEndPoint 类的新实例。              
                    //listenSocket    
                    listenSocket.Bind(ep);        //要与 Socket 关联的本地 EndPoint。                  
                    listenSocket.Listen(5);
                    //将 Socket 置于侦听状态,参数backlog为挂起连接队列的最大长度          
                    //accept the call    
                    Socket mySoket = listenSocket.Accept();  //为新建连接创建新的 Socket。
                    Console.WriteLine("已建立联接!");                    //define a buff    
                    Byte[] buff = new Byte[256];
                    int result;
                    //new file    
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }
                    //StreamWrite    
                    StreamWriter sr = new StreamWriter(File.Create(filename));
                    while (true)
                    {
                        buff = new Byte[256];
                        result = mySoket.Receive(buff);  //接收来自绑定的 Socket 的数据。                      
                        sr.Write(Encoding.Default.GetString(buff));
                        //sr.Write();                       
                        if (result < 256)
                            break;
                    }
                    sr.Close();//close buffer write    

                    //ConState.Clear();     
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("yichang");
                    Console.WriteLine(ex.ToString());
                }


        }
    }
}






          // FileStream FStream = File.OpenWrite("E://计算机学院//班长工作7班//活动//124.mp4");
            //FileStream fs = File.OpenRead("E://计算机学院//班长工作7班//活动//123.mp4");
            //byte[] b = new byte[1024];
            //while(fs.Read(b,0,b.Length)>0){
            //    FStream.Write(b, 0, b.Length);
            //}
        

