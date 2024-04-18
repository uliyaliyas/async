using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 54321;
IPAddress address = IPAddress.Parse("127.0.0.1");
Console.WriteLine("Введите сообщение:");
string Msg = Console.ReadLine();
var msg = new string[] { "Hallo server | Connect me", "Work mith me! Trust", "TERMINATE" };
var i = 0;
while(i<Msg.Length)
{
    using (TcpClient client = new TcpClient())
    {
        client.Connect(address, port);
        if(client.Connected)
        {
            Console.WriteLine("Connected");
        }
        var bytes = Encoding.UTF8.GetBytes(msg[i++]);
        using(var requestStream=client.GetStream())
        {
            await requestStream.WriteAsync(bytes, 0, bytes.Length);
            var requesBytes = new byte[256];
            await requestStream.ReadAsync(requesBytes, 0, requesBytes.Length);
            var ResponceMsg = Encoding.UTF8.GetString(requesBytes);
            Console.WriteLine();
            Console.WriteLine("Responce from server:");
            Console.WriteLine(ResponceMsg);
        }
    }
    var sleepDuration = new Random().Next(2000, 10000);
    Console.WriteLine($"Generaytion a new request in {sleepDuration / 1000} seconds");
    Thread.Sleep(sleepDuration);
}