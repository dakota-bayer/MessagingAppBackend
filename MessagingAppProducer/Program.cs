namespace MessagingAppProducer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

// Add services to the container
        builder.Services.AddControllers(); // Registers controllers with dependency injection
        builder.Services.AddEndpointsApiExplorer(); // Optional for API endpoint discovery
        builder.Services.AddSwaggerGen(); // Optional for Swagger

        var app = builder.Build();

// Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection(); // Redirect HTTP to HTTPS

        app.UseRouting(); // Enables routing middleware

        app.UseAuthorization(); // Handles authorization policies

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Maps API controllers with attribute routing
        });

        app.Run();
    }
}