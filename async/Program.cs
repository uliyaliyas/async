using async;

Server s = new Server("192.168.113.2", 1024);
s.Start();
Console.Read();
s.Stop();
