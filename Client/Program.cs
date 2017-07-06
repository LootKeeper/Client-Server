using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress _ipAdress = IPAddress.Parse("127.0.0.1");
            int port = 8005;
            string msg = "";

            try
            {                                
                IPEndPoint ipEndPoint = new IPEndPoint(_ipAdress, port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                while (!socket.Connected)
                {
                    try
                    {
                        socket.Connect(ipEndPoint);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    System.Threading.Thread.Sleep(5000);
                }

                Console.WriteLine("Connected success.");
                
                Console.WriteLine("!--------------------------------------------------------------------------!");
                Console.WriteLine("Enter 0(zero) for exit.\r\n");

                byte[] data;

                while (msg != "0")
                {
                    Console.Write("Enter a message to send: ");
                    msg = Console.ReadLine();

                    data = Encoding.Unicode.GetBytes(msg);
                    socket.Send(data);
                }
                data = Encoding.Unicode.GetBytes(msg);
                socket.Send(data);

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadKey();
        }
    }
}
