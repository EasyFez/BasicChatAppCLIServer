using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var webSocketServer = new WebSocketServer("ws://localhost:8080");
            webSocketServer.AddWebSocketService<ChatBehaviour>("/Chat");
            webSocketServer.Start();
            Console.WriteLine($"Chat App Server Started at  {webSocketServer.Port}");
            Console.WriteLine("Press Any Key Sto Stop The Server!");

            Console.ReadKey();
            webSocketServer.Stop();
        }
    }


    public class ChatBehaviour : WebSocketBehavior
    {
       
    }
}