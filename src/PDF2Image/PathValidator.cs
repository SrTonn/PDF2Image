namespace PDF2Image;

public static class PathValidator
{
    public static bool IsFilePath(string path) => !string.IsNullOrWhiteSpace(path) && Path.HasExtension(path);

    public static bool IsDirectoryPath(string path) => !string.IsNullOrWhiteSpace(path) && !Path.HasExtension(path) && Directory.Exists(path);
}