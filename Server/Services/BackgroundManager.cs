using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Services.Interfaces;
using Server.Shared;

namespace Server.Services;

public class BackgroundManager(
    ILogger<BackgroundManager> logger,
    IConfiguration conf,
    IHostApplicationLifetime appLifeTime,
    IDirectoryService service) : BackgroundService
{
    const string BeginningLog = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var sw = Stopwatch.StartNew();
        sw.Start();
        var workingDir = new DirectoryInfo(conf["WorkingDir"].ToCrossPlatformDirectory());
        logger.LogInformation("{Beginning} Starting...", BeginningLog);
        await service.WriteDirectoryListAsync(workingDir);
        logger.LogInformation("{Beginning} Elapsed={Elapsed}", BeginningLog, sw.Elapsed);

        appLifeTime.StopApplication();
    }
}
