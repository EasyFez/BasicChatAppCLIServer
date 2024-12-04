using System;
using System.Diagnostics;
using WebSocketSharp;
using WebSocketSharp.Server;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace ChatServer
{
    class Program
    {


         static void Main(string[] args)
        {
             int port;


            Console.WriteLine("Enter Port Number To Start The Server");
            string portString = Console.ReadLine();
            
            if(int.TryParse(portString, out port) == false)
            {
                Console.WriteLine("Invalid Port Number, Please Try Again!");
                return;
            }

            var webSocketServer = new WebSocketServer($"ws://127.0.0.1:{port}");
            webSocketServer.Start();
            webSocketServer.AddWebSocketService<ChatBehaviour>("/chat");

            //This is only hardcoded now because it's always going to be hosted on localhost
            Console.WriteLine($"Chat App Server Started at  127.0.0.1:{webSocketServer.Port}");
            

            Console.WriteLine("Press Any Key To Stop The Server!");

            Console.ReadKey();
            webSocketServer.Stop();
            Console.WriteLine("Server Successfully Stopped!");

        }



    }


    public class ChatBehaviour : WebSocketBehavior
    {
       protected override void OnMessage(MessageEventArgs e)
        {
            Sessions.Broadcast($"From: {ID}: " + e.Data);
            Console.WriteLine($"Received Message: {e.Data}");
        }

        protected override void OnOpen()
        {
            Console.WriteLine($"Client Connected: {ID}");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine($"Client Disconnected: {ID}");
        }

        protected override void OnError(ErrorEventArgs e)
        {
            Console.WriteLine($"Error: {e}");
            
        }
    }
}