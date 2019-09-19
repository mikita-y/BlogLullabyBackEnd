using BlogLullaby.BLL.EmailService;
using BlogLullaby.WEB_API.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BlogLullaby.DAL.SqlServerDataStore.Context;
using BlogLullaby.DAL.DataStore.Interfaces;
using BlogLullaby.DAL.IdentityManager.Interfaces;
using BlogLullaby.DAL.DataStore.Repositories;
using Microsoft.AspNetCore.Identity;
using logLullaby.DAL.AspNetCoreIdentityManager.Context;
using BlogLullaby.DAL.AspNetCoreIdentityManager.Repositories;

namespace BlogLullaby.WEB_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmailConfig>(Configuration.GetSection("EmailConfig"));
            services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));
            
            services.AddDbContext<SqlServerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"), b => b.MigrationsAssembly("BlogLullaby.DAL.SqlServerDataStore")));
            services.AddDbContext<IdentitySqlServerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentitySqlServerConnection")));
 
            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<IdentitySqlServerContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IDataStore, SqlServerDataStore>();
            services.AddTransient<IIdentityManager, AspNetCoreIdentityManager>();
            services.AddBLLServices();          
            services.AddTransient<FileSavingHelper>();

            services.AddCors();
            services.AddJWTAuthentication(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseCors(builder =>
            builder.AllowAnyOrigin()//WithOrigins(Configuration["CorsOrigins:Host1"], Configuration["CorsOrigins:Host2"])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action?}/{id?}");
            });
        }
    }
}
