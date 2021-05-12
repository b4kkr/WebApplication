using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using AutoMapperBuilder.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CarMechanicContext>(
                o =>
                { 
                    o.UseMySql(
                        "server=localhost;port=3306;database=carmechanic;user=admin;password=admin",
                        new MySqlServerVersion(new Version(8, 0, 23)));
                    o.EnableSensitiveDataLogging();
                });
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IRepairService, RepairService>();
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
            
            //ToDo: hibakezelést ide kell implementálni

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}