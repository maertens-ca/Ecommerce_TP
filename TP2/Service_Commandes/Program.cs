using Service_Comandes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseRouting();
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Commandes API"));
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}

app.Run();
