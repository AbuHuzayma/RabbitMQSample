// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;


//Run 
example3();

void example2()
{
    ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
    using (var connection = factory.CreateConnection())
    using (var channal = connection.CreateModel())
    {
        string message = "Hello World!";
        var body = Encoding.UTF8.GetBytes(message);
        channal.BasicPublish(exchange: "",
            routingKey: "",
            basicProperties: null,
            body: body);

        Console.WriteLine(" [x] Sent {0}",message);
        Console.WriteLine("Press Enter To Exit");
        Console.ReadLine();
    }

}

void example3()
{
    ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
    using (var connection = factory.CreateConnection())
    using (var channal = connection.CreateModel())
    {
        for (int i = 0; i < 100; i++)
        {
            if (i % 2 == 0)
            {
                string message = $"Log Info '{i}'";
                var body = Encoding.UTF8.GetBytes(message);
                channal.BasicPublish(exchange: "exTopic",
                    routingKey: "log.Info",
                    body: body);

            }
            else
            {
                string message = $"Log Error '{i}'";
                var body = Encoding.UTF8.GetBytes(message);
                channal.BasicPublish(exchange: "exTopic",
                    routingKey: "log.Error",
                    body: body);
            }
            Console.WriteLine(" [x] Sent {0}", i);
            Thread.Sleep(2000);
        }

        Console.WriteLine("Press Enter To Exit");
        Console.ReadLine();
    }
}
