using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Swagger;
using Users.API.Infrastructure;
using Users.API.Infrastructure.Repositories;
using Users.API.Infrastructure.Services;

namespace Users.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add framework services.
            services.AddSwaggerGen(options =>
            {
                //options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "TestOnContainers - User HTTP API",
                    Version = "v1",
                    Description = "The User Microservice HTTP API. This is a Data-Driven/CRUD microservice sample",
                    TermsOfService = new Uri("https://example.com/terms")
                });
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            //});

            //services.Configure<UserSettings>(Configuration);
            services.Configure<UserSettings>(Configuration.GetSection(nameof(UserSettings)));
            //services.AddSingleton<IUserSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<UserSettings>>().Value);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IUsersRepository, UsersRepository>();

            services.AddTransient<IUserSettings, UserSettings>();
            //services.AddTransient<IUsersContext, UsersContext>();            
            //services.AddScoped<IMongoDatabase>(serviceProvider =>
            //{                
            //    //var settings = serviceProvider.GetRequiredService<IUserSettings>();
            //    //var client = new MongoClient(settings.Value.ConnectionString);
            //    //return client.GetDatabase(settings.Value.Database);

            //    //var dbParams = serviceProvider.GetRequiredService<IUserSettings>();
            //    //var dbParams = serviceProvider.GetService<UserSettings>();
            //    var dbParams = serviceProvider.GetRequiredService<IOptions<UserSettings>>();
            //    var client = new MongoClient(dbParams.Value.ConnectionString);
            //    return client.GetDatabase(dbParams.Value.Database);
            //});

            services.AddSingleton<IMongoClient>(s => new MongoClient(Configuration.GetConnectionString("MongoDb")));
            services.AddScoped(s => new UsersContext(s.GetRequiredService<IMongoClient>(), Configuration["Database"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var pathBase = Configuration["PATH_BASE"];

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });            

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
               {
                   //c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Users.API V1");                   
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users.API V1");
               });

            //UsersContextSeed.SeedAsync(app, loggerFactory).Wait();

            //var usersContext = app.ApplicationServices.GetRequiredService<IUsersContext>();
            //var mongoDb = app.ApplicationServices.GetRequiredService<IMongoDatabase>();
            //var usersContextSeed = new UsersContextSeed(usersContext, mongoDb);
            //var usersContextSeed = new UsersContextSeed(usersContext, null);
            //usersContextSeed.SeedAsync(app, loggerFactory).Wait();

            PrepareDatabase(app, loggerFactory);
        }

        public void PrepareDatabase(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            try
            {
                using (var scope = app.ApplicationServices.CreateScope())
                //using (var context = scope.ServiceProvider.GetService<UsersContext>())
                //using (var context = scope.ServiceProvider.GetRequiredService<IUsersContext>())                
                {
                    var context = new UsersContext(scope.ServiceProvider.GetRequiredService<IMongoClient>(), Configuration["Database"]);

                    //var usersContextSeed = new UsersContextSeed(context, null);
                    var client = scope.ServiceProvider.GetRequiredService<IMongoClient>();
                    var usersContextSeed = new UsersContextSeed(context, client, Configuration["Database"]);
                    usersContextSeed.SeedAsync(app, loggerFactory).Wait();
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
