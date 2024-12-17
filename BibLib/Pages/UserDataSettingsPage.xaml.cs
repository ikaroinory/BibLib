using System.Windows;
using System.Windows.Controls;
using BibLib.ViewModels;

namespace BibLib.Pages;

public partial class UserDataSettingsPage : Page
{
    public UserDataSettingsPage()
    {
        InitializeComponent();

        var viewModel = new SettingsViewModel
        {
            UserDataPath = GlobalResources.BibLibFolderPath,
            ApplicationConfigurationPath = GlobalResources.ConfigurationFilePath,
            DatabasePath = GlobalResources.DatabaseFilePath,
        };

        DataContext = viewModel;
    }

    private void BrowseButton_OnClick(object sender, RoutedEventArgs e) =>
        System.Diagnostics.Process.Start("explorer.exe", GlobalResources.BibLibFolderPath);
}
