namespace Server.Services.Interfaces;

public interface IDirectoryService
{
    public Task WriteDirectoryListAsync(DirectoryInfo directory);
}
