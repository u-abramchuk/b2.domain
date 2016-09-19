using System.IO;
using b2.Domain.CommandHandlers;
using b2.Domain.Core;
using b2.Domain.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace b2.Domain.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                            .AddCommandLine(args)
                            .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                            .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<KnownEvents>();

            services.AddSingleton<IEventStore>(
                container => new EventStore(
                    Configuration.GetConnectionString("EventStore"), 
                    container.GetService<KnownEvents>()
                )
            );
            services.AddSingleton<IEventPublisher>(
                container => new EventPublisher(
                    Configuration.GetConnectionString("RabbitMQ"), 
                    container.GetService<KnownEvents>()
                )
            );
            services.AddSingleton<Repository>();
            // services.AddSingleton<TaskCommandHandler>();
            // services.AddSingleton<BranchCommandHandler>();
            // services.AddSingleton<WorkItemCommandHandler>();
            services.AddSingleton<WorkspaceCommandHandler>();
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory
        )
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}