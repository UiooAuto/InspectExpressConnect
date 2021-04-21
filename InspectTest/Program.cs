using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace InspectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             //IP地址的ping测试
             Ping ping = new Ping();
            PingReply pingReply = ping.Send("39.156.66.19");
            if (pingReply.Status ==IPStatus.Success)
            {
                Console.WriteLine("Ping Success");
            }
            else
            {
                Console.WriteLine("Ping Fail");
            }

            Console.ReadKey();*/
            var serverIP = "127.0.0.1";
            var serverPORT = 5024;
            var cmd = "cmd;";
            byte[] resBytes = new byte[1024];
            var recStr = "";

            var ip = IPAddress.Parse(ipString: serverIP);

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                clientSocket.Connect(new IPEndPoint(ip, serverPORT));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("连接成功");
            Console.WriteLine("__________");

            try
            {
                var sendBytes = Encoding.UTF8.GetBytes(cmd);
                clientSocket.Send(buffer: sendBytes);

                Console.WriteLine("Send Over");

                int bytes = clientSocket.Receive(resBytes, resBytes.Length, 0);
                recStr += Encoding.UTF8.GetString(resBytes, 0, bytes);
                Console.WriteLine("RecOver");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            while (true)
            {
                var sendBytes1 = Encoding.UTF8.GetBytes(cmd);
                clientSocket.Send(buffer: sendBytes1);

                Console.WriteLine("Send Over");
                
                recStr = "";
                int bytes1 = clientSocket.Receive(resBytes, resBytes.Length, 0);
                recStr += Encoding.UTF8.GetString(resBytes, 0, bytes1);
                Console.WriteLine(recStr);
            }


            Console.WriteLine(recStr);
            Console.ReadKey();
        }
    }
}