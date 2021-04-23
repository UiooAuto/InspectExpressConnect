using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static InspectTest.InspectUtils;

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
                sendCmdToInspect(socket, "cmd;");
                recStr = receiveDataFromInspect(socket, resBytes);
                Console.WriteLine(recStr);
            }*/
            int count = 0;
            while (true)
            {
                var dataFromInspect = receiveDataFromInspect(socket, resBytes);
                if (dataFromInspect == "01WRDD5950 01\r\n")
                {
                    count++;
                    if (count == 19)
                    {
                        Console.WriteLine(dataFromInspect + "---read");
                        sendCmdToInspect(socket, "11OK0001"+'\r'+'\n');
                        Thread.Sleep(500);
                        recStr = receiveDataFromInspect(socket, resBytes);
                        Console.WriteLine(recStr + "---Rec1");
                        recStr = receiveDataFromInspect(socket, resBytes);
                        Console.WriteLine(recStr + "---Rec2");
                        count = 0;
                    }
                }
            }
        }
    }
}