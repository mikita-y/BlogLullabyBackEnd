using BlogLullaby.WEB_API.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using BlogLullaby.DAL.AspNetCoreIdentityManager.Context;

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
            services.AddConfigs(Configuration);
            services.AddBLLServices();
            services.AddWebApiServices();
            services.AddDbContexts(Configuration);
            services.AddDataStores();
            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<IdentitySqlServerContext>()
                .AddDefaultTokenProviders();
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
