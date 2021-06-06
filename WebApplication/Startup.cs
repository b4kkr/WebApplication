using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication.Mapper;
using WebApplication.Repository;
using WebApplication.Service;
using WebApplication.Service.Impl;

namespace WebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IWebHostEnvironment Environment { get; set; }
        public IConfiguration Configuration { get; set; }
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;

        }
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var mySqlVersion = new MySqlServerVersion(new Version(8, 0, 23));
            services.AddDbContext<CarMechanicContext>(opt =>
            {
                opt.UseMySql(connectionString, mySqlVersion);
                opt.EnableSensitiveDataLogging();
                opt.EnableDetailedErrors();
            });
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IRepairService, RepairService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<CarMechanicContext, CarMechanicContext>();
            services.AddControllers().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
            services.AddAutoMapper(typeof(CarMechanicMapper));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}