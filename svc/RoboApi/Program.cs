using System.Text.Json.Serialization;

namespace RoboApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
            );

        builder.Services.AddOpenApi();

        // Adicionar serviço CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseCors("AllowAngular");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
