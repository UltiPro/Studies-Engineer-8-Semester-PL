using RabbitMQ.Client;
using System.Text;

try
{
    while (true)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");

            var routingKey = "sample.key";
            var message = Console.ReadLine();
            var body = Encoding.UTF8.GetBytes(message ?? "Any message");

            channel.BasicPublish(exchange: "topic_logs",
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($"[x] Sent '{routingKey}':'{message}'");
        }
    }
}
catch
{
    return;
}