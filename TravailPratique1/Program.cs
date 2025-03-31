using Stripe;
using TravailPratique1.Models;

namespace TravailPratique1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            builder.Services.AddSwaggerGen();

            //Préparation Stripe
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

            var app = builder.Build();

            

            

            //Condig Swagger
            
            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TravailPratique1 v1"));
            }

            // Création routes
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "Default",
                    "{controller=Home}/{Action=Index}");
            });

            app.UseFileServer();
            app.Run();
        }
    }
}
