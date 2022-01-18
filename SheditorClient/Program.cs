using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sheditor
{
    class Program
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        int pos = Console.CursorLeft;
        
        static void sendToServer(string textToSend)
        {
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            Console.Clear();
            Console.Write(Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            client.Close();
        }

    static void Main(string[] args)
        {
            ConsoleKeyInfo info;
            char input;
            List<char> chars = new List<char>();
            int pos = Console.CursorLeft;
            string textToSend = "";
            while (true)
            {
                info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Backspace/* && Console.CursorLeft > pos*/)
                {
                    //Console.WriteLine(textToSend);
                    textToSend = "USUWAJ";
                    sendToServer(textToSend);
                }
                else if (info.Key == ConsoleKey.Enter)
                {
                    Console.Write('\n');
                    chars.Add('\n');
                    textToSend = "ENTERUJ";
                    sendToServer(textToSend);
                }
                else if (char.IsLetterOrDigit(info.KeyChar))
                {
                    input = info.KeyChar;
                    //chars.Add(info.KeyChar);
                    textToSend = info.KeyChar.ToString();
                    sendToServer(textToSend);
                }
                //else if (info.Key == ConsoleKey.CursorLeft) {
                //    Console.CursorLeft -= 1;
                //}
                else if (info.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if(info.Key == ConsoleKey.Spacebar)
                {
                    textToSend = " ";
                    sendToServer(textToSend);
                }
                else
                {
                    Console.WriteLine("Nie ma takiego znaku");
                    //textToSend = "";
                }
                
            }
        }
    }
}