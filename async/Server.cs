using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace async
{
    internal class Server
    {
        delegate void ConnectDelegate(Socket s);
        delegate void StartNetwork(Socket s);
        Socket? socket;
        IPEndPoint? endP;

        public Server(string strAddr, int port)
        {
            this.endP = new IPEndPoint(IPAddress.Parse(strAddr), port);
        }
        void Server_Connect(Socket s)
        {
            s.Send(System.Text.Encoding.Unicode.GetBytes(DateTime.Now.ToString()));
            s.Shutdown(SocketShutdown.Both);
            s.Close();
        }
        void Server_Begin(Socket s)
        {
            while (true)
            {
                try
                {
                    while (s != null)
                    {
                        Socket ns = s.Accept();
                        Console.WriteLine(ns.RemoteEndPoint!.ToString());
                        ConnectDelegate cd = new ConnectDelegate(Server_Connect);
                        cd.BeginInvoke(ns, null, null);
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void Start()
        {
            if (socket != null) return;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.IP);
            socket.Bind(endP!);
            socket.Listen(10);
            StartNetwork start = new StartNetwork(Server_Begin);
            start.BeginInvoke(socket, null, null);
        }
        public void Stop()
        {
            if (socket != null)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    socket = null;
                }
                catch (SocketException ex) { }
            }
        }
    }
}