using Stripe;
using TravailPratique1.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            


            // Configure EF Core
            builder.Services.AddDbContext<BoutiqueDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configure Identity
            builder.Services.AddIdentity<Models.User, IdentityRole>()
                .AddEntityFrameworkStores<BoutiqueDbContext>()
                .AddDefaultTokenProviders();

            // Configure JWT Authentication
            var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        ValidateLifetime = true
                    };
                });

            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            // Middleware
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();



            //Condig Swagger

            if (app.Environment.IsDevelopment())
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

            /* 
            app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
            */

            app.UseFileServer();
            app.Run();
        }
    }
}
