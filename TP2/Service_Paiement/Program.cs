using Service_Paiement.Models;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

var app = builder.Build();

app.UseMvc(routes => routes.MapRoute("Default", "{controller=Payment}/{action=Index}"));
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Panier API"));
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();
