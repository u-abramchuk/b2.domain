using System.IO;
using System.Threading.Tasks;
using b2.Domain.CommandHandlers;
using b2.Domain.Core;
using EventStore.ClientAPI;
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

            var eventStoreConnection = InitializeEventStoreConnection().Result;
            services.AddSingleton<IEventStoreConnection>(_ => eventStoreConnection);

            services.AddSingleton<IEventStore, PersistentEventStore>();
            services.AddSingleton<Repository>();
            services.AddSingleton<TaskCommandHandler>();
            services.AddSingleton<BranchCommandHandler>();
            services.AddSingleton<WorkItemCommandHandler>();
        }

        private async Task<IEventStoreConnection> InitializeEventStoreConnection()
        {
            var eventStoreConnection = await EventStoreConnection.Create(Configuration.GetConnectionString("EventStore"));

            await eventStoreConnection.ConnectAsync();

            return eventStoreConnection;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Tasks}/{action=Test}");
            });
        }
    }
}