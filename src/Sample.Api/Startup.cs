using System.Collections.Generic;
using System.Data.Common;
 using Microsoft.eShopOnContainers.BuildingBlocks.IntegrationEventLogEF.Services;
 
 namespace Sample.Api
 {
     using System;
     using System.Data.SqlClient;
     using System.IO;
     using System.Net;
     using System.Reflection;
     using System.Threading.Tasks;
     using Autofac;
     using Autofac.Extensions.DependencyInjection;
     using AutoMapper;
     using Microsoft.AspNetCore.Builder;
     using Microsoft.AspNetCore.Diagnostics;
     using Microsoft.AspNetCore.Hosting;
     using Microsoft.AspNetCore.Http;
     using Microsoft.EntityFrameworkCore;
     using Microsoft.eShopOnContainers.BuildingBlocks.EventBus;
     using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
     using Microsoft.eShopOnContainers.BuildingBlocks.EventBusRabbitMQ;
     using Microsoft.eShopOnContainers.BuildingBlocks.EventBusServiceBus;
     using Microsoft.eShopOnContainers.BuildingBlocks.IntegrationEventLogEF;
     using Microsoft.Extensions.Configuration;
     using Microsoft.Extensions.DependencyInjection;
     using Microsoft.Extensions.HealthChecks;
     using Microsoft.Extensions.Logging;
     using Microsoft.Extensions.PlatformAbstractions;
     using Newtonsoft.Json.Converters;
     using Newtonsoft.Json.Serialization;
     using Polly;
     using Sample.Api.Attributes;
     using Sample.Api.Events;
     using Sample.Api.Extensions;
     using Sample.Api.Filters;
     using Sample.Api.Infrastructure;
     using Sample.Api.Infrastructure.Filters;
     using Sample.Api.Modules;
     using Sample.Api.Profiles;
     using Sample.API.Infrastructure.AutofacModules;
 // using Sample.Concrete.Modules;
 // using Sample.Concrete.Profiles;
 // using Sample.Concrete.Repository;
 // using Sample.Concrete.Repository.Modules;
     using Sample.Infrastructure;
     using Swashbuckle.AspNetCore.Swagger;
 
     public class Startup
     {
         public Startup(IHostingEnvironment env)
         {
             var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .AddEnvironmentVariables();
 
             if (env.IsDevelopment())
             {
                 builder.AddUserSecrets(typeof(Startup).GetTypeInfo().Assembly);
             }
 
             Configuration = builder.Build();
         }
 
         public IConfigurationRoot Configuration { get; }
 
         // This method gets called by the runtime. Use this method to add services to the container.
         public IServiceProvider ConfigureServices(IServiceCollection services)
         {
             // Add Service Layer Dependencies
             /* services.AddSingleton<IArtistService, ArtistService>();
             services.AddSingleton<Sample.Concrete.Repository.IBaseRepository<Concrete.Repository.Entities.Person>, Sample.Concrete.Repository.BaseRepository<Concrete.Repository.Entities.Person>>();
             services.AddSingleton<Microsoft.EntityFrameworkCore.DbContext, Concrete.Repository.SampleContext>(); */
 
             ConfigureAutoMapper();
 
             //services.AddApplicationInsightsTelemetry(Configuration);
 
             // Add framework services.
             services
                 .AddMvc(options =>
                 {
                     options.RespectBrowserAcceptHeader = true;
                     options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                     //options.Filters.Add(new ValidateModelAttribute());
                     //options.Filters.Add(new ExceptionHandlerFilter());
                 })
                 .AddJsonOptions(options =>
                 {
                     options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
                     options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                     options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                     options.SerializerSettings.Converters.Add(new StringEnumConverter() {CamelCaseText = false});
                     options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
                     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                     options.SerializerSettings.PreserveReferencesHandling =
                         Newtonsoft.Json.PreserveReferencesHandling.None;
                 })
                 .AddControllersAsServices();
 
             services.AddHealthChecks(checks =>
             {
                 var minutes = 1;
                 if (int.TryParse(Configuration["HealthCheck:Timeout"], out var minutesParsed))
                 {
                     minutes = minutesParsed;
                 }
                 checks.AddSqlCheck("SampleDb", Configuration["ConnectionString"], TimeSpan.FromMinutes(minutes));
             });
 
             services.AddEntityFrameworkSqlServer()
                 .AddDbContext<SampleContext>(options =>
                     {
                         options.UseSqlServer(Configuration["ConnectionString"],
                             sqlServerOptionsAction: sqlOptions =>
                             {
                                 sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                 sqlOptions.EnableRetryOnFailure(
                                     maxRetryCount: 5,
                                     maxRetryDelay: TimeSpan.FromSeconds(30),
                                     errorNumbersToAdd: null
                                 );
                             });
                     },
                     //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                     ServiceLifetime.Scoped
                 );
 
             services.Configure<SampleSettings>(Configuration);
 
             // Register the Swagger generator, defining one or more Swagger documents
             services.AddSwaggerGen(options =>
             {
                 options.SwaggerDoc("v1", new Info
                 {
                     Version = "v1",
                     Title = "Microservice API",
                     Description = "An example ASP.NET Core Web API using CQRS and DDD pattern",
                     TermsOfService = "None",
                     Contact = new Contact
                     {
                         Name = "Ruddy Cahyadi",
                         Email = "ruddycahyadi@gmail.com",
                         Url = "https://github.com/rdyc"
                     },
                     License = new License {Name = "Use under LICX", Url = "https://example.com/license"}
                 });
                 
                 options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                 {
                     Type = "oauth2",
                     Flow = "implicit",
                     AuthorizationUrl = $"{Configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize",
                     TokenUrl = $"{Configuration.GetValue<string>("IdentityUrlExternal")}/connect/token",
                     Scopes = new Dictionary<string, string>()
                     {
                         //{ "openid", "OpenID" },
                         { "api1", "API v1" }
                     }
                 });
 
                 // Set the comments path for the Swagger JSON and UI.
                 var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                 var xmlPath = Path.Combine(basePath, "Sample.Api.xml");
                 options.IncludeXmlComments(xmlPath);
                 
                 options.OperationFilter<AuthorizeCheckOperationFilter>();
             });
 
             // Add application services.
             services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
             //services.AddTransient<IIdentityService, IdentityService>();
             services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                 sp => (DbConnection c) => new IntegrationEventLogService(c));
 
             //services.AddTransient<IOrderingIntegrationEventService, OrderingIntegrationEventService>();
 
             RegisterEventBus(services);
 
             services.AddOptions();
 
             var container = new ContainerBuilder();
             container.Populate(services);
 
             container.RegisterModule(new MediatorModule());
             container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));
 
             return new AutofacServiceProvider(container.Build());
         }
 
         // ConfigureContainer is where you can register things directly
         // with Autofac. This runs after ConfigureServices so the things
         // here will override registrations made in ConfigureServices.
         // Don't build the container; that gets done for you. If you
         // need a reference to the container, you need to use the
         // "Without ConfigureContainer" mechanism shown later.
         public void ConfigureContainer(ContainerBuilder builder)
         {
             // builder.RegisterModule<RepositoryAutofacModule>();
             // builder.RegisterModule<ConcreteAutofacModule>();
             // builder.RegisterModule<ApiAutofacModule>();
             //builder.RegisterModule<EventModule>();
         }
 
         // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
         public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
         {
             loggerFactory.AddConsole(Configuration.GetSection("Logging"));
             loggerFactory.AddDebug();
 
             if (env.IsDevelopment())
             {
                 app.UseDeveloperExceptionPage();
 
                 ConfigureSwagger(app);
             }
 
             /* app.UseExceptionHandler(options => {
                  
                  options.Run(
                      async context =>
                      {
                          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                          context.Response.ContentType = "text/html";
                          var ex = context.Features.Get<IExceptionHandlerFeature>();
                          if (ex != null)
                          {
                              var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
                              await context.Response.WriteAsync(err).ConfigureAwait(false);
                          }
                      });
              });*/
 
             app.UseExceptionHandler(o => o.UseExceptionHandlerMiddleware());
 
             //app.UseSampleMiddleware();
             /*app.UseStatusCodePages(async context =>
             {
                 context.HttpContext.Response.ContentType = "text/plain";
 
                 await context.HttpContext.Response.WriteAsync(
                     "Status code page, status code: " +
                     context.HttpContext.Response.StatusCode);
             });*/
 
             app.UseMvcWithDefaultRoute();
 
             WaitForSqlAvailabilityAsync(loggerFactory, app, env).Wait();
 
             var integrationEventLogContext = new IntegrationEventLogContext(
                 new DbContextOptionsBuilder<IntegrationEventLogContext>()
                     .UseSqlServer(Configuration["ConnectionString"], b => b.MigrationsAssembly("Sample.Api"))
                     .Options);
             integrationEventLogContext.Database.Migrate();
 
             //ConfigureEventBus(app);
         }
 
         private static void ConfigureAutoMapper()
         {
             Mapper.Initialize(cfg =>
             {
                 cfg.AddProfile<ApiMapperProfile>();
                 //cfg.AddProfile<ConcreteMapperProfile>();
             });
         }
 
         private static void ConfigureSwagger(IApplicationBuilder app)
         {
             app.UseSwagger()
                 .UseSwaggerUI(c =>
                 {
                     c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                     //c.ConfigureOAuth2("swaggerui", "", "", "Swagger UI", " ");
                     c.ConfigureOAuth2("js", "", "", "JavaScript Client", " ");
                 });
         }
 
         /* private void ConfigureEventBus(IApplicationBuilder app)
         {
             var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
 
             eventBus.Subscribe<UserCheckoutAcceptedIntegrationEvent, IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>>();
             eventBus.Subscribe<GracePeriodConfirmedIntegrationEvent, IIntegrationEventHandler<GracePeriodConfirmedIntegrationEvent>>();
             eventBus.Subscribe<OrderStockConfirmedIntegrationEvent, IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>>();
             eventBus.Subscribe<OrderStockRejectedIntegrationEvent, IIntegrationEventHandler<OrderStockRejectedIntegrationEvent>>();
             eventBus.Subscribe<OrderPaymentFailedIntegrationEvent, IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>>();
             eventBus.Subscribe<OrderPaymentSuccededIntegrationEvent, IIntegrationEventHandler<OrderPaymentSuccededIntegrationEvent>>();
         } */
 
         private void RegisterEventBus(IServiceCollection services)
         {
             if (Configuration.GetValue<bool>("AzureServiceBusEnabled"))
             {
                 services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
                 {
                     var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                     var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                     var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                     var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                     var subscriptionClientName = Configuration["SubscriptionClientName"];
 
                     return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                         eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
                 });
             }
             else
             {
                 services.AddSingleton<IEventBus, EventBusRabbitMQ>();
             }
 
             services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
         }
 
         private async Task WaitForSqlAvailabilityAsync(ILoggerFactory loggerFactory, IApplicationBuilder app,
             IHostingEnvironment env, int retries = 0)
         {
             var logger = loggerFactory.CreateLogger(nameof(Startup));
             var policy = CreatePolicy(retries, logger, nameof(WaitForSqlAvailabilityAsync));
             await policy.ExecuteAsync(async () => { await SampleContextSeed.SeedAsync(app, env, loggerFactory); });
         }
 
         private Policy CreatePolicy(int retries, ILogger logger, string prefix)
         {
             return Policy.Handle<SqlException>().WaitAndRetryAsync(
                 retryCount: retries,
                 sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                 onRetry: (exception, timeSpan, retry, ctx) =>
                 {
                     logger.LogTrace(
                         $"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                 }
             );
         }
     }
 }