using Pastel;
using System.Net;
using System.Net.Sockets;
using static System.Console;

namespace bartolini.liam._5h.esercitazioneSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("I'm a Server!");
            const int PORT = 9000, BACKLOG = 128;
            
            IPEndPoint endPoint = new(IPAddress.Loopback, PORT);
            Socket socket = new(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(endPoint);
            socket.Listen(BACKLOG);
            
            WriteLine("Pending...".Pastel("#FFFF00"));
            Socket clientSocket = socket.Accept();
            WriteLine("Connection with client done!".Pastel("#00FF00"));
            
            while(true)
            {        
                byte[] buffer = new byte[1024];
                int numberOfByte = clientSocket.Receive(buffer);

                // 0..numberOfByte serve per andare a selezionare solamente l'intervallo della parola inserita
                string msg = System.Text.Encoding.UTF8.GetString(buffer[0..numberOfByte]);
                if (string.IsNullOrEmpty(msg))
                {
                    clientSocket.Close();
                    WriteLine("\nClient disconnected!".Pastel("#FF0000"));
                    break;
                }
                Write($"{"message received:".PadRight(25)} {msg.Pastel("#7DF9FF")}");

                clientSocket.Send(buffer[0..numberOfByte]);
                string msgSent = System.Text.Encoding.UTF8.GetString(buffer[0..numberOfByte]);
                Write($"{"message sent:".PadRight(25)} {msgSent.Pastel("#7DF9FF")}");
            }
        }
    }
}