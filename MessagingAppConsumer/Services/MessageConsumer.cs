using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using MessagingAppConsumer.Data;
using SharedModels;

namespace MessagingAppConsumer.Services;

public class MessageConsumer
{
    private readonly string _queueName = "messages";
    private readonly string _hostName = "localhost";

    public async Task StartListeningAsync()
    {
        var factory = new ConnectionFactory { HostName = _hostName };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var messageJson = Encoding.UTF8.GetString(body);

            // Deserialize the message
            var message = JsonSerializer.Deserialize<Message>(messageJson);
            if (message != null)
            {
                await SaveMessageToDbAsync(message);
            }
        };

        await channel.BasicConsumeAsync(queue: _queueName,
            autoAck: true,
            consumer: consumer);

        Console.WriteLine("Waiting for messages...");
        await Task.Delay(-1);
    }

    private async Task SaveMessageToDbAsync(Message message)
    {
        await using var context = new MessagingDbContext();
        context.Messages.Add(message);
        var result = await context.SaveChangesAsync();
        if (result == 1)
        {
            Console.WriteLine($"[x] Saved message: {message.Content}");
        }
        else
        {
            Console.WriteLine($"[x] Failed to save message: {message.Content}");
        }
    }
}