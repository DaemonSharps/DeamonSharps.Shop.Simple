using DeamonSharps.Shop.Simple.Api.Services;
using DeamonSharps.Shop.Simple.DataBase.Context;
using DeamonSharps.Shop.Simple.Services;
using DeamonSharps.Shop.Simple.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace DeamonSharps.Shop.Simple
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string XMLPath
        {
            get
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                return xmlPath;
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ShopDBContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<ProductServiceController>();
            services.AddTransient<CategoryServiceController>();
            services.AddTransient<OrderServiceController>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc(
                    "v1.0",
                    new OpenApiInfo
                    {
                        Title = "DaemonSharps.Shop.BuisnessLogicServices",
                        Version = "1.0"
                    });
                setup.CustomSchemaIds(type => type.ToString());
                setup.IncludeXmlComments(XMLPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseFileServer();

            app.UseSession();
            app.UseSwagger();
            app.UseRouting();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "BusinessLogicServices");
                options.RoutePrefix = "swagger";
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
