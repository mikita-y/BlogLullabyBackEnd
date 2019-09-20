using BlogLullaby.BLL.AuthenticationService;
using BlogLullaby.BLL.EmailService;
using BlogLullaby.BLL.PostPreviewListService;
using BlogLullaby.BLL.PostService;
using BlogLullaby.BLL.UserCommunicatingService;
using BlogLullaby.BLL.UserListService;
using BlogLullaby.BLL.UserProfileService;
using BlogLullaby.DAL.SqlServerDataStore.Context;
using BlogLullaby.DAL.AspNetCoreIdentityManager.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BlogLullaby.DAL.DataStore.Interfaces;
using BlogLullaby.DAL.DataStore.Repositories;
using BlogLullaby.DAL.IdentityManager.Interfaces;
using BlogLullaby.DAL.AspNetCoreIdentityManager.Repositories;

namespace BlogLullaby.WEB_API.Infrastructure
{
    public static class ServiceProviderExtensions
    {
        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostPreviewListService, PostPreviewListService>();
            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<IUserListService, UserListService>();
            services.AddTransient<IUserCommunicatingService, UserCommunicatingService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAuthenticationService, AuthenticationService >();
        }

        public static void AddWebApiServices(this IServiceCollection services)
        {
            services.AddTransient<OnlineRefresher>();
            services.AddTransient<FileSavingHelper>();
        }

        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqlServerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"), b => b.MigrationsAssembly("BlogLullaby.DAL.SqlServerDataStore")));
            services.AddDbContext<IdentitySqlServerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentitySqlServerConnection")));
        }

        public static void AddDataStores(this IServiceCollection services)
        {
            services.AddTransient<IDataStore, SqlServerDataStore>();
            services.AddTransient<IIdentityManager, AspNetCoreIdentityManager>();
        }

        public static void AddConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailConfig>(configuration.GetSection("EmailConfig"));
            services.Configure<JWTConfig>(configuration.GetSection("JWTConfig"));
            services.Configure<AppConfig>(configuration.GetSection("AppConfig"));
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
