using System.Globalization;

namespace Server.Shared;

public static class Utils
{
    public static string BytesToString(this long byteCount)
    {
        string[] suf = ["B", "KB", "MB", "GB", "TB", "PB", "EB"];
        if (byteCount == 0)
        {
            return $"0{suf[0]}";
        }

        long bytes = Math.Abs(byteCount);
        int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
        double num = Math.Round(bytes / Math.Pow(1024, place), 1);
        return (Math.Sign(byteCount) * num).ToString(CultureInfo.InvariantCulture) + suf[place];
    }

    public static string ToCrossPlatformDirectory(this string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        return input.Replace("~", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), StringComparison.Ordinal);
    }
}
