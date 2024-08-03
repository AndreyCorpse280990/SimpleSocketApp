using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSocketApp_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start client ...");
            Console.ReadKey(true);

            // ОБЩИЙ АЛГОРИТМ КЛИЕНТА (ЛИНЕЙНЫЙ)
            // 1. Создать и сконфигурировать сокет клиента
            // 2. Инициировать подключение к серверу
            // 3. Получить сообщение от сервера
            // 4. Отправить ответ серверу
            // 5. Закрыть соединение, завершить взаимодействие

            // РЕАЛИЗАЦИЯ АЛГОРИТМ

            // 1. задать параметры сервера для подключения к нему
            string serverIpStr = "127.0.0.1"; // localhost
            int serverPort = 1024;

            // 2. создать сокет клиента
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            Console.WriteLine("[client]: client created and configured");

            // 3. инициировать подключение к серверу
            Console.WriteLine("[client]: try connect to server ...");
            client.Connect(serverIpStr, serverPort);
            Console.WriteLine($"[client]: connection established successfully with {serverIpStr}:{serverPort}");

            // 4. получить сообщение от сервера
            byte[] buf = new byte[1024];    // буфер для получения сообщения
            int bytesReceived = client.Receive(buf);            // чтение сообщения от сервера
            string message = Encoding.UTF8.GetString(buf, 0, bytesReceived);
            Console.WriteLine($"[client]: received '{message}'");

            // 5. отправить сообщение серверу
            message = "hello from client";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            client.Send(messageBytes);
            Console.WriteLine($"[client]: sent '{message}'");

            // 6. закрыть соединение с сервером и освободить ресурсы
            client.Shutdown(SocketShutdown.Both);
            client.Dispose();
            Console.WriteLine("[client]: connection closed, client disposed");
        }
    }
}
