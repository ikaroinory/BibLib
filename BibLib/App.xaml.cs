using System.IO;
using System.Text.Json;
using System.Windows;
using BibLib.ViewModels;

namespace BibLib;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App() => InitializeApplication();

    private static void InitializeApplication()
    {
        InitializeFolders();

        InitializeConfiguration();

        InitializeDatabase();
    }

    private static void InitializeFolders()
    {
        if (Directory.Exists(GlobalResources.BibLibFolderPath)) return;

        Directory.CreateDirectory(GlobalResources.BibLibFolderPath);
    }

    private static void InitializeConfiguration()
    {
        if (!File.Exists(GlobalResources.ConfigurationFilePath))
        {
            var configJsonString = JsonSerializer.Serialize(
                GlobalResources.Configuration,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    IndentSize = 4
                }
            );

            File.WriteAllText(GlobalResources.ConfigurationFilePath, configJsonString);
        }

        var json = File.ReadAllText(GlobalResources.ConfigurationFilePath);

        GlobalResources.SetConfiguration(json);
    }

    private static void InitializeDatabase()
    {
        if (File.Exists(GlobalResources.DatabaseFilePath)) return;

        using var dataBase = new ApplicationDatabase();
        dataBase.Database.EnsureCreated();
    }
}
