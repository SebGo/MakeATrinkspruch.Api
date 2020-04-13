using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace MakeATrinkspruch.Api
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
            ConfigureSwagger(services);
            ConfigureDatabaseService(services);
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new OpenApiInfo
               {
                   Title = Properties.Resources.ApiTitle,
                   Version = Properties.Resources.ApiVersion,

                   Description = Properties.Resources.ApiDescription,

                   License = new OpenApiLicense
                   {
                       Name = "Use under GPLv3",
                       Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.html"),
                   }
               });
           });
        }

        private void ConfigureDatabaseService(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("MakeATrinkspruchConnection");
            services.AddDbContext<AppDBContext>(options => options.UseMySql(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Properties.Resources.ApiTitle} {Properties.Resources.ApiVersion}");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}