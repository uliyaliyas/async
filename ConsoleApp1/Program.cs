using System.Net;
using System.Net.Sockets;
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
socket.Bind(new IPEndPoint(IPAddress.Parse("192.168.113.2"),100));

//int l = socket.ReceiveFrom(buffer, ref ep);
string ipclient = ((IPEndPoint)EndPoint);