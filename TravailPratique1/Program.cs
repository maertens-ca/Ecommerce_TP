using Stripe;
using TravailPratique1.Models;

namespace TravailPratique1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
            var app = builder.Build();
            

            // Préparation Stripe
            //builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            //StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

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
