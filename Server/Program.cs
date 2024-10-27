using Server;
using WebSocketSharp.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var server = new WebSocketServer("ws://localhost:4649");

        //string cert = "***CERTIFICATE PATH***";
        //string password = "***PWD***";

        //server.SslConfiguration.ServerCertificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(cert, password);

        server.AddWebSocketService<Echo>("/Echo");

        server.Start();

        if (server.IsListening)
        {
            Console.WriteLine($"Listening on port {server.Port}!!");
        }

        Console.WriteLine("Enter key to stop the server!");
        Console.ReadLine();

        server.Stop();
    }
}