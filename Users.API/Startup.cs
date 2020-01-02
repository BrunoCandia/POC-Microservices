using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Users.API.Filters;
using Users.API.Infrastructure;
using Users.API.Infrastructure.Mongo;
using Users.API.Infrastructure.Persistence;
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

            services.AddApiVersioning(options => {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");             
            });

            MongoDbPersistence.Configure();

            // Add framework services.
            services.AddSwaggerGen(options =>
            {
                //options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "TestOnContainers - User HTTP API",
                    Version = "1",
                    Description = "The User Microservice HTTP API. This is a Data-Driven/CRUD microservice sample",
                    TermsOfService = new Uri("https://example.com/terms")
                });

                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "TestOnContainers - User HTTP API",
                    Version = "2",
                    Description = "The User Microservice HTTP API. This is a Data-Driven/CRUD microservice sample",
                    TermsOfService = new Uri("https://example.com/terms")
                });

                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var actionApiVersionModel = (ApiVersionModel)apiDesc.ActionDescriptor?.Properties.FirstOrDefault(w => ((Type)w.Key).Equals(typeof(ApiVersionModel))).Value;

                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }

                    return actionApiVersionModel.DeclaredApiVersions.Any()
                                                ? actionApiVersionModel.DeclaredApiVersions.Any(version => $"v{version.ToString()}".Equals(docName))
                                                : actionApiVersionModel.ImplementedApiVersions.Any(version => $"v{version.ToString()}".Equals(docName));
                });

                options.DocumentFilter<AddVersionHeader>();
            });            

            services.Configure<UserSettings>(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IUsersRepository, UsersRepository>();

            services.AddTransient<IQuestionsService, QuestionsService>();
            services.AddTransient<IQuestionsRepository, QuestionsRepository>();

            services.AddScoped<IMongoContext, MongoContext>();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {            
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
                   c.SwaggerEndpoint("/swagger/v2/swagger.json", "Users.API V2");
                   c.RoutePrefix = string.Empty;                   
               });

            PrepareDatabase(app, loggerFactory);
        }

        public void PrepareDatabase(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            try
            {
                using (var scope = app.ApplicationServices.CreateScope())                
                {
                    var settings = scope.ServiceProvider.GetRequiredService<IOptions<UserSettings>>();

                    var context = new MongoContext(settings);                                        
                    var usersContextSeed = new UsersContextSeed(context);
                    usersContextSeed.SeedAsync(app, loggerFactory).Wait();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
