using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.DbOperations;
using WebApi.Middlewares;
using WebApi.Services;

namespace WebApi
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
            
            // Adds BookStoreDbContext to the services container
            // An in memory database has been used in this project
            services.AddDbContext<BookStoreDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
            
            // Adds IBookStoreDbContext interface to the services container and 
            // provides an instance of BookStoreDbContext each http request life time
            services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());
            
            // Adds AutoMapper to the services container
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            // Adds a ILoggerService to the container and returns a ConsoleLogger object 
            // It just creates one instance of ConsoleLogger at the beginning of runtime and sends it when requested
            services.AddSingleton<ILoggerService, ConsoleLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
    
            // Adds CustomExceptionMiddleware to the pipeline 
            // just before the endpoints middleware
            // in this way all exceptions thrown from endpoints
            // will be caught in the CustomExceptionMiddleWare
            app.UseCustomException(); 
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
