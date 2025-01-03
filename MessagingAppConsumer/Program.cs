using MessagingAppConsumer.Services;

namespace MessagingAppConsumer;

class Program
{
    static async Task Main(string[] args)
    {
        var consumer = new MessageConsumer();
        await consumer.StartListeningAsync();

        Console.WriteLine("Consumer is running. Press [enter] to exit.");
        Console.ReadLine();
    }
}
