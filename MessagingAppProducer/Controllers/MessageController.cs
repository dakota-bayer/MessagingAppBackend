using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MessagingAppProducer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly ConnectionFactory _connectionFactory;

    public MessageController()
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = "localhost" // Adjust this to your RabbitMQ broker's hostname
        };
    }

    [HttpPost]
    public async Task<IActionResult> PublishMessageAsync([FromBody] MessageDto message)
    {
        try
        {
            await using var connection = await _connectionFactory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            // Declare a queue (if not already declared)
            await channel.QueueDeclareAsync(queue: "messages",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Serialize the message to JSON
            var messageBody = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageBody);

            // Publish the message
            await channel.BasicPublishAsync(exchange: "",
                routingKey: "messages",
                body: body);

            return Accepted(new { message = "Message published successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}

public class MessageDto
{
    public string Content { get; set; } = "";
    public DateTime Timestamp { get; set; }
}