using System.Text.Json;
using BibLib.Models;

namespace BibLib.ViewModels;

public static class GlobalResources
{
    public static string UserFolderPath => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    public static string BibLibFolderPath => Path.Combine(UserFolderPath, ".biblib");
    public static string ConfigurationFilePath => Path.Combine(BibLibFolderPath, "config.json");
    public static string DatabaseFilePath => Path.Combine(BibLibFolderPath, "lib.db");

    public static Configuration Configuration { get; set; } = new();

    public static void SetConfiguration(string configurationJson) => Configuration = JsonSerializer.Deserialize<Configuration>(configurationJson)!;
}
