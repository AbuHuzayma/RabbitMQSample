// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

consumeExchangeMessage();

void consumeExchangeMessage()
{
    ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
    using (var connection = factory.CreateConnection())
    using (var channal = connection.CreateModel())
    {
        var consumer = new EventingBasicConsumer(channal);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Message:{message} has been consumed by '{ea.ConsumerTag}'");
            Console.WriteLine("==========================================================");
        };
        string message = "Hello World!";
        var body = Encoding.UTF8.GetBytes(message);
        channal.BasicConsume(queue: "log.Info",
            autoAck: true,// delete message after readed and send read acknowlgment
            consumer: consumer,
            consumerTag: "INFO");

        channal.BasicConsume(queue: "log.Error",
            autoAck: true,
            consumer: consumer,
            consumerTag: "Error");

        channal.BasicConsume(queue: "log.all",
            autoAck: true,
            consumer: consumer,
            consumerTag: "ALL");

        Console.WriteLine("Press Enter To Exit");
        Console.ReadLine();
    }
}