using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public static class Core
    {
        static bool _isContinue;

        static TcpListener _server;
        static int _port;

        public static void Start()
        {
            _server = new TcpListener(IPAddress.Any, _port);

            try
            {
                _server.Start();
                // Use async, coze we using async methods in it.
                Task.Run(async() => StartAcceptAsync(_server).Wait());
                Logger.Message("Server start at: " + DateTime.Now.ToString() + "\r\n");

                Console.WriteLine("!--------------------------------------------------------------------------!");
                Console.WriteLine("1: Stop.\r\n" +
                                  "2: Exit.\r\n");

                // Main loop, it's give us opportunity stop server.
                while (_isContinue)
                {
                    _isContinue = Console.ReadLine() == "1" ? IsStop() : false;
                }               
            }
            catch (Exception ex)
            {
                Logger.Message("Error: " + ex.ToString());
                Environment.Exit(0);
            }
            finally
            {
                _server.Stop();
            }     
        }

        static bool IsStop()
        {
            _server.Stop();
            Console.WriteLine("Press any button to continue");
            Console.ReadLine();

            return true;
        }

        static async Task StartAcceptAsync(TcpListener listener)
        {
            while (_isContinue)
            {
                Socket client = await listener.AcceptSocketAsync();
                Console.WriteLine("Client has connected");
                var task = ProcessAcceptAsync(client);
                if (task.IsFaulted)
                    task.Wait();
            }
        }

        static Task ProcessAcceptAsync(Socket client)
        {
            // Make some action, registr clint or something else.
            //
            //
            // Read incoming stream.
            return Task.Run(() => ReadIncomingStreamAsync(client));
        }

        static async Task ReadIncomingStreamAsync(Socket stream)
        {
            byte[] _buff = new byte[256];
            int _bytes;

            while (_isContinue)
            {
                try
                {
                    _bytes = stream.Receive(_buff);
                    string output = Encoding.Unicode.GetString(_buff, 0, _bytes);
                    if (output == "0")
                    {
                        stream.Shutdown(SocketShutdown.Both);
                        stream.Close();
                        Console.WriteLine("Client has been disconected");
                        break;
                    }

                    Console.WriteLine(output);
                }
                catch(Exception ex)
                {
                    Logger.Message("Error: "+ ex.Message);
                }
            }
        }

        static Core()
        {
            _isContinue = true;
            _port = 8005;
        }
    }
}
