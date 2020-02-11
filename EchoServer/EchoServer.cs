using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace EchoServer
{
    class EchoServer
    {
        static void Main(string[] args)
        {
            //Opret listener og starte
            IPAddress ip = IPAddress.Parse("192.168.24.241");
            TcpListener serverSocket = new TcpListener(ip, 7777);

            serverSocket.Start();
            Console.WriteLine("Server Started");

            //Venter på connection før den aktiverer
            TcpClient connectionSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine("Server activated & Connected");

            DoClient(connectionSocket);
            serverSocket.Stop();
            Console.WriteLine("Server stopped & Disconnected");
        }

        public static void DoClient(TcpClient socket)
        {
            Stream ns = socket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            string message = sr.ReadLine();
            string answer = "";

            while (message != null && message != "")
            {
                Console.WriteLine("Client: " + message);
                answer = message.ToUpper();
                sw.WriteLine(answer);
                message = sr.ReadLine();
            }

            ns.Close();
            socket.Close();
        }
    }
}
