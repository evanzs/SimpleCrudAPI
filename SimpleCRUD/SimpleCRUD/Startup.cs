using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SimpleCRUD.Context;
using SimpleCRUD.Data;
using System;
using System.Text.Json.Serialization;
using Amazon.S3;
using SimpleCRUD.Models;

namespace SimpleCRUD
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
            string npgConnection = Configuration.GetConnectionString("Connection");

            services.AddDbContext<LibraryContext>(c =>
            {
                try
                {
                    c.UseLazyLoadingProxies().UseNpgsql(npgConnection);
                }
                catch (Exception e)
                {
                    var message = e.Message;
                }

            });

            //dependencias       
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.Configure<S3Configuration>(Configuration.GetSection("S3Config"));

         

            services.AddControllers()
                .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                     
                 });
          
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimpleCRUD", Version = "v1" });
              

            });

      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleCRUD v1"));
                
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
