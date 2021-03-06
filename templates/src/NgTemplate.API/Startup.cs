using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NgTemplate.API.Configurations;
using NgTemplate.API.Data;
using NgTemplate.API.Repositories;
using NgTemplate.API.Services;

namespace NgTemplate.API
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NgTemplate.API", Version = "v1" });
            });

            var mySqlConfig = Configuration.GetSection("MySqlConnection").Get<DbConfigModel>();
            services.AddDbContext<AppDBContext>(options =>
            options.UseMySql(
                mySqlConfig.ConnectionString,
                new MySqlServerVersion(new Version(mySqlConfig.MajorVersion, mySqlConfig.MinorVersion, mySqlConfig.Build)),
                mySqlOptions =>
                {
                   mySqlOptions.EnableRetryOnFailure();
                }));

            services.AddScoped<IDemoRepository, DemoRepository>();
            services.AddScoped<IDemoService, DemoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("./v1/swagger.json", "IntegrationService.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
