using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSocketApp_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start server ...");
            Console.ReadKey(true);

            // ОБЩИЙ АЛГОРИТМ СЕРВЕРА (ЛИНЕЙНЫЙ)
            // 1. Создать и сконфигурировать сокет сервера
            // 2. Начать ожидание входящего подключения
            // 3. Отправить сообщение подключенному клиенту
            // 4. Получить ответ клиента
            // 5. Закрыть соединение, завершить взаимодействие

            // РЕАЛИЗАЦИЯ АЛГОРИТМ

            // 1. задать параметры сервера - ip-адрес и порт для сокета сервера
            string serverIpStr = "127.0.0.1"; // localhost
            int serverPort = 1024;

            // 2. создать и настроить сокет сервера
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIpStr), serverPort);
            server.Bind(serverEndPoint);    // связывание сокета сервера с endpoint-ом сервера
            server.Listen(1);               // перевод сокета сервера в режим прослуживание входящих подключений
            Console.WriteLine($"[server]: server created and configured on {serverEndPoint}");

            // 3. начать ожидать входящее подключение
            Console.WriteLine("[server]: waiting incoming connection ...");
            Socket client = server.Accept();    // в этот момент программа заблокируется до подключения клиента
            Console.WriteLine($"[server]: connection established successfully with {client.RemoteEndPoint}");

            // 4. отправить сообщение подключенному клиенту
            string message = $"Hello from server! Now UTC time is {DateTime.UtcNow}";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            client.Send(messageBytes);
            Console.WriteLine($"[server]: sent '{message}'");

            // 5. получить сообщение от клиента
            byte[] buf = new byte[1024];
            int bytesReceived = client.Receive(buf);
            message = Encoding.UTF8.GetString(buf, 0, bytesReceived);
            Console.WriteLine($"[server]: received '{message}'");

            // 6. закрыть соединение с клиентом и освободить ресурсы
            client.Shutdown(SocketShutdown.Both); // закрываем соединение с клиентом
            client.Dispose();
            server.Dispose();
            Console.WriteLine("[server]: connection closed, server and client disposed");
        }
    }
}
