using BlogLullaby.BLL.AuthenticationService;
using BlogLullaby.BLL.EmailService;
using BlogLullaby.BLL.PostPreviewListService;
using BlogLullaby.BLL.PostService;
using BlogLullaby.BLL.UserCommunicatingService;
using BlogLullaby.BLL.UserProfileService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogLullaby.WEB_API.Infrastructure
{
    public static class ServiceProviderExtensions
    {
        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostPreviewListService, PostPreviewListService>();
            services.AddTransient<IUserProfileService, UserProfileService>(); 
            services.AddTransient<IUserCommunicatingService, UserCommunicatingService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAuthenticationService, AuthenticationService >();
            services.AddTransient<OnlineRefresher>();
        }

        public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration appConfig)
        {
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = appConfig["JWTConfig:Issuer"],
                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = appConfig["JWTConfig:Audience"],
                            // будет ли валидироваться время существования
                            ValidateLifetime = false,
                            // установка ключа безопасности
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appConfig["JWTConfig:SecurityKey"])),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });
        }
    }
}
