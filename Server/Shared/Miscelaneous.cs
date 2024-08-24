namespace Server.Shared;

public static class Miscelaneous
{
    public static void GetStuff(DirectoryInfo directory)
    {
        var crapList = directory.EnumerateFiles("*", SearchOption.AllDirectories)
            .Where(x => string.Equals(x.Name, "Thumbs.db", StringComparison.Ordinal));
        foreach (var fi in crapList)
        {
            // fi.Delete();
            Console.WriteLine($"SUPPR : {fi.FullName}");
        }

        var oldestFile = directory.EnumerateFiles("*", SearchOption.AllDirectories).MinBy(f => f.LastAccessTime);
        var largestFile = directory.EnumerateFiles("*", SearchOption.AllDirectories).MaxBy(f => f.Length);

        Console.WriteLine("");
        Console.WriteLine("Oldest file");
        Console.WriteLine(oldestFile?.Name);
        Console.WriteLine(oldestFile?.LastAccessTime);
        Console.WriteLine(oldestFile?.Length);
        Console.WriteLine(oldestFile?.Length.BytesToString());

        Console.WriteLine("");
        Console.WriteLine("Largest file");
        Console.WriteLine(largestFile?.Name);
        Console.WriteLine(largestFile?.LastAccessTime);
        Console.WriteLine(largestFile?.Length);
        Console.WriteLine(largestFile?.Length.BytesToString());
    }
}
