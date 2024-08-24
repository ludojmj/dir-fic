using Microsoft.Extensions.Configuration;
using Server.Services.Interfaces;
using Server.Shared;

namespace Server.Services;

public class DirectorysService(IConfiguration conf) : IDirectoryService
{
    public async Task WriteDirectoryListAsync(DirectoryInfo directory)
    {
        var maxLoop = conf.GetSection("MaxLoop").Get<int>();
        var dirOut = conf["OutputDir"].ToCrossPlatformDirectory();
        var fileOut = conf["OutputFile"] ?? string.Empty;
        var fullpathOut = Path.Combine(dirOut, fileOut);

        var text = $"FullName;LastAccessTime;Length;Human length{Environment.NewLine}";
        await File.WriteAllTextAsync(fullpathOut, text);

        var dirList = directory.EnumerateDirectories("*", SearchOption.AllDirectories)
            .OrderByDescending(x => x.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length))
            .Take(maxLoop);

        var resultList = new List<string>();
        foreach (var di in dirList)
        {
            var dirSize = di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
            Console.WriteLine($"{di.FullName} : {dirSize.BytesToString()}");
            var line = string.Join(';', di.FullName, di.LastAccessTime, dirSize, dirSize.BytesToString());
            resultList.Add(line);
        }

        await File.AppendAllLinesAsync(fullpathOut, resultList);
    }
}
