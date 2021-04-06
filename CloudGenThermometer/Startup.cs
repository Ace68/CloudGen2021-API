using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.IO;
using CloudGenThermometer.Mediator;
using CloudGenThermometer.Mediator.Subscribers;
using CloudGenThermometer.ReadModel.MongoDb;
using CloudGenThermometer.Shared.Configuration;

namespace CloudGenThermometer
{
    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(this.Configuration["CloudGen:Serilog:PathLog"])
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", corsBuilder =>
                    corsBuilder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
            });
            #endregion

            #region MVC
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            services.AddMvc(option => option.EnableEndpointRouting = false);
            #endregion

            #region Configuration
            services.Configure<ApiSettings>(options =>
                this.Configuration.GetSection("CloudGen").Bind(options));
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GlobalAzure #2021", Version = "v1", Description = "Web Api Services for Thermometer" });

                var pathDoc = "CloudGenThermometer.xml";

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, pathDoc);
                if (File.Exists(filePath))
                    c.IncludeXmlComments(filePath);
            });
            #endregion

            #region CustomServices
            services.AddDeviceMessagesProcessor(this.Configuration["CloudGen:AzureServiceBusParameters:PrimaryConnectionString"]);
            services.AddFactories();
            services.AddEventHandler();
            services.AddApplicationServices();

            var mongoDbParameters = new MongoDbParameters();
            this.Configuration.GetSection("CloudGen:MongoDbParameters").Bind(mongoDbParameters);
            services.AddMongoDb(mongoDbParameters);
            #endregion

            StartSubscribers.Start(services);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
            IHostApplicationLifetime applicationLifetime)
        {
            #region Environment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            #endregion

            #region Logging
            loggerFactory.AddSerilog();

            // Ensure any buffered events are sent at shutdown
            applicationLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
            #endregion

            #region CORS
            app.UseCors("CorsPolicy");
            #endregion

            #region Https
            app.UseHttpsRedirection();
            #endregion

            #region DefaultIndex
            app.UseDefaultFiles();
            #endregion

            #region Endpoints
            app.UseFileServer();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            #endregion

            #region Swagger
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "documentation/{documentName}/documentation.json";
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/documentation/v1/documentation.json", "Thermometer Web API");
            });
            #endregion
        }
    }
}
