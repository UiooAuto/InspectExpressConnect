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
            var serverIp = "127.0.0.1";
            var serverPort = 5024;
            var cmd = "StartInspect;";
            byte[] resBytes = new byte[1024];
            var recStr = "";

            var socket = connectToInspect(serverIp, serverPort);
            /*while (true)
            {
                sendCmdToInspect(socket, "start");
                var dataFromInspect = receiveDataFromInspect(socket, resBytes);
                Console.WriteLine(dataFromInspect);
            }*/


            while (true)
            {
                var cmdToInspect = sendCmdToInspect(socket,"cmd;");
                var dataFromInspect = receiveDataFromInspect(socket,resBytes);
                Console.WriteLine(dataFromInspect);
            }
        }

        
        /*
         * 用于连接Inspect软件
         */
        public static Socket connectToInspect(string serverIp, int serverPort)
        {
            IPAddress ipAddress;
            Socket socket;
            if (serverIp != null && serverPort != 0)
            {
                ipAddress = IPAddress.Parse(serverIp);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socket.Connect(new IPEndPoint(ipAddress, serverPort));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }

                return socket;
            }
            else
            {
                return null;
            }
        }
        
        /*
         用于关闭连接
         */
        public static void shutDownInspect(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
        }

        /*
         * 用于向Inspect发送指令
         */
        public static string sendCmdToInspect(string serverIP, int serverPORT, string cmd)
        {
            Socket socket;
            var cmdBytes = Encoding.UTF8.GetBytes(cmd);
            if (serverIP != null && serverPORT != 0 & cmd != null)
            {
                socket = connectToInspect(serverIP,serverPORT);
                if (socket == null)
                {
                    return "Connect_Fail";
                }
                try
                {
                    socket.Send(cmdBytes);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return "send_Fail";
                }
                return "Send_Successful";
            }
            return "Parameter_Null";
        }
        public static string sendCmdToInspect(Socket socket, string cmd)
        {
            var cmdBytes = Encoding.UTF8.GetBytes(cmd);
            if (socket!= null & cmd != "")
            {
                try
                {
                    socket.Send(cmdBytes);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return "send_Fail";
                }
                return "Send_Successful";
            }
            return "Parameter_Null";
        }
        
        
        /*
         * 用于从Inspect接收数据
         */
        public static string receiveDataFromInspect(string serverIp, int serverPort, byte[] recBytes)
        {
            Socket socket;
            string recStr = "";
            if (serverIp != null && serverPort != 0 & recBytes != null)
            {
                socket = connectToInspect(serverIp,serverPort);
                if (socket == null)
                {
                    return "Connect_Fail";
                }
                try
                {
                    var receiveCode = socket.Receive(recBytes,recBytes.Length,0);
                    recStr += Encoding.UTF8.GetString(recBytes,0,receiveCode);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return "Receive_Fail";
                }
                return "Receive_Successful";
            }
            return "Parameter_Null";
        }
        public static string receiveDataFromInspect(Socket socket,byte[] recBytes)
        {
            string recStr = "";
            if (socket!=null&recBytes != null)
            {
                try
                {
                    var receiveCode = socket.Receive(recBytes,recBytes.Length,0);
                    recStr += Encoding.UTF8.GetString(recBytes,0,receiveCode);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return "Receive_Fail";
                }
                return recStr;
            }
            return "Parameter_Null";
        }
    }
}