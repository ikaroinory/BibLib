using System.Windows;

namespace BibLib.Views;

public partial class SettingsView : Window
{
    public SettingsView() => InitializeComponent();

    private void UserDataTreeViewItem_OnSelected(object sender, RoutedEventArgs e) =>
        MainContent.Source = new Uri("../Pages/UserDataSettingsPage.xaml", UriKind.Relative);

    private void WelcomeTreeViewItem_OnSelected(object sender, RoutedEventArgs e) =>
        MainContent.Source = new Uri("../Pages/WelcomeSettingsPage.xaml", UriKind.Relative);
}
