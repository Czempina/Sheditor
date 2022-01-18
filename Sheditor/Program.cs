using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace Sheditor
{
    class Program
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";

        static void Main(string[] args)
        {
            string path = "C:/Users/Czemp/OneDrive/Pulpit/BIP/PolitechnikaPoznańska/Sieci V/Czempina/Sheditor/test.txt";

            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            string text = File.ReadAllText(path);

            while (true)
            {
                Console.WriteLine("Listening...");
                listener.Start();
                //---incoming client connected---
                TcpClient client = listener.AcceptTcpClient();

                //---get the incoming data through a network stream---
                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //---convert the data received into a string---
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                if (dataReceived == "USUWAJ" && text.Length > 0)
                {
                    text = text.Remove(text.Length - 1, 1);
                }
                else if (dataReceived == "ENTERUJ")
                {
                    text = text + '\n';
                }
                else
                {
                    text = text + dataReceived;
                }

                Console.WriteLine("text : " + text);

                byte[] bytesWrite = Encoding.ASCII.GetBytes(text);

                //---write back the text to the client---
                Console.WriteLine("Sending back : " + text);
                nwStream.Write(bytesWrite, 0, bytesWrite.Length);
                client.Close();
                listener.Stop();
                File.WriteAllText(path, text);
            }
            Console.ReadLine();
        }
    }
}
