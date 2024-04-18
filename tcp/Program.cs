using System.Net;
using System.Net.Sockets;
using System.Text;

bool done = false;
string DILIMETR = "|";
string TERNINATE = "TERMINATE";
int port = 54321;
IPAddress addres = IPAddress.Any;
TcpListener server = new TcpListener(addres, port);
server.Start();
var loggedNoRequiest = false;
while(!done)
{
    if (!server.Pending())
    {
        if (!loggedNoRequiest)
        {
            Console.WriteLine();
            Console.WriteLine("No pendiing request");
            Console.WriteLine("Server listening");
            loggedNoRequiest = true;
        }
    }
    else
    {
        loggedNoRequiest = false;
        byte[] bytes = new byte[256];
        using (var client = await server.AcceptTcpClientAsync()) 
        {
            using (var tcpStream = client.GetStream())
            {
                await tcpStream.ReadAsync(bytes, 0, bytes.Length);
                var requestMsg = Encoding.UTF8.GetString(bytes).Replace("\0", string.Empty);
                if (requestMsg.Equals(TERNINATE))
                {
                    done = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Message for client:");
                    Console.WriteLine(requestMsg);
                    var playLoad = requestMsg.Split(DILIMETR).Last();
                    var responseMsg = $"Greeting from the server! | {playLoad}";
                    var responseBytes = Encoding.UTF8.GetBytes(responseMsg);
                    await tcpStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                }
            }
        }
    }
}
server.Stop();
Thread.Sleep(1000);