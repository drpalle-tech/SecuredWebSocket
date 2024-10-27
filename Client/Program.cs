using WebSocketSharp;

public class Program
{
    public static void Main (string[] args)
    {
        using (var client = new WebSocket("wss://localhost:4649/Echo"))
        {
            client.OnOpen += (sender, e) =>
            {
                client.Send("Client says Hi!");
            };

            client.OnMessage += (sender, e) =>
            {
                string data = !e.IsPing ? e.Data : "Server pings!!";
                Console.WriteLine(data);
            };

            client.OnError += (sender, e) =>
            {
                Console.WriteLine($"ERROR in socket with message: {e.Message}");
            };

            client.OnClose += (sender, e) =>
            {
                Console.WriteLine($"Connection closed with code: {e.Code} and reason: {e.Reason}");
            };

            //Validating the server certificate..
            client.SslConfiguration.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => {
                    var data = "Certificate:\n- Issuer: {0}\n- Subject: {1}";
                    var msg = String.Format(data, certificate.Issuer, certificate.Subject);
                    // If the server certificate is valid.
                    // Must perform security validations based on the client's customization.
                    // No security checks will be an anti-pattern.
                    return true; 
            };

            client.Connect();

            Console.WriteLine("Type EXIT to exit the client.\n");

            while (true)
            {
                Thread.Sleep(1000);

                Console.Write("Client > ");
                var input = Console.ReadLine();

                if(input == "EXIT")
                    break;

                client.Send(input);
            }
        }
    }
}
