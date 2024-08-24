using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Services;
using Server.Services.Interfaces;

using IHost host = CreateHostBuilder(args).Build();
await host.RunAsync();
return;

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices(services =>
        {
            services.AddHostedService<BackgroundManager>();
            services.AddTransient<IDirectoryService, DirectorysService>();
        })
        .ConfigureAppConfiguration(_ => {});
