using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

try
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using (var connection = factory.CreateConnection())
    using (var channel = connection.CreateModel())
    {
        channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");

        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue: queueName,
                          exchange: "topic_logs",
                          routingKey: "sample.key");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var routingKey = ea.RoutingKey;
            Console.WriteLine($"[x] Received '{routingKey}':'{message}'");
        };
        channel.BasicConsume(queue: queueName,
                             autoAck: true,
                             consumer: consumer);
        while (true)
        {
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
catch
{
    return;
}