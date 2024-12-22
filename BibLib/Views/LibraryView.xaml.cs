using System.Windows;
using System.Windows.Controls;
using BibLib.Dialogs;
using BibLib.Utils;
using BibLib.ViewModels;

namespace BibLib.Views;

public partial class LibraryView : Window
{
    private readonly LibraryViewModel _viewModel;

    public LibraryView()
    {
        InitializeComponent();

        _viewModel = new LibraryViewModel();
        DataContext = _viewModel;

        ArticleBibliographyDataGrid.ItemsSource = _viewModel.Articles;
        BookBibliographyDataGrid.ItemsSource = _viewModel.Books;
    }

    private void BibliographyDataGrid_OnLoadingRow(object? sender, DataGridRowEventArgs e) => e.Row.Header = (e.Row.GetIndex() + 1).ToString();

    private void AddBibliographyMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        var addBibliographyDialog = new AddBibliographyDialog();
        if (addBibliographyDialog.ShowDialog() == false) return;
    }

    private void AddBibliographyFromBibTeXMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        var addBibliographyFromBibTeXDialog = new AddBibliographyFromBibTeXDialog();
        if (addBibliographyFromBibTeXDialog.ShowDialog() == false) return;

        var bibliographies = new BibTeXParser(addBibliographyFromBibTeXDialog.BibTexText).Parse();

        _viewModel
            .AddRange(bibliographies)
            .ToList()
            .ForEach(pair =>
            {
                if (pair.Value == 0) return;

                var word = pair.Value == 1 ? "has" : "have";
                MessageBox.Show($"{pair.Value} {pair.Key} {word} been inserted.");
            });

        _viewModel.Refresh();
    }

    private void RemoveFromLibraryMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        _viewModel.RemoveRange().ToList().ForEach(pair =>
        {
            if (pair.Value == 0) return;

            var word = pair.Value == 1 ? "has" : "have";
            MessageBox.Show($"{pair.Value} {pair.Key} {word} been removed.");
        });

        _viewModel.Refresh();
    }

    private void SettingsMenuItem_OnClick(object sender, RoutedEventArgs e) => new SettingsView().ShowDialog();

    private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

    private void ExportToBibTeXMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        var exportToBibTeXDialog = new ExportToBibTeXDialog
        {
            BibTexTextBox =
            {
                Text = _viewModel.Export()
            }
        };
        exportToBibTeXDialog.ShowDialog();
    }

    private void SelectAllMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedItem = (TabItem)LibraryTabControl.SelectedItem;

        if (selectedItem == ArticleTabItem)
            _viewModel.Articles.ToList().ForEach(bib => bib.IsSelected = true);
        else if (selectedItem == BookTabItem)
            _viewModel.Books.ToList().ForEach(bib => bib.IsSelected = true);


        ArticleBibliographyDataGrid.Items.Refresh();
    }

    private void DeselectAllMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedItem = (TabItem)LibraryTabControl.SelectedItem;

        if (selectedItem == ArticleTabItem)
            _viewModel.Articles.ToList().ForEach(bib => bib.IsSelected = false);
        else if (selectedItem == BookTabItem)
            _viewModel.Books.ToList().ForEach(bib => bib.IsSelected = false);


        ArticleBibliographyDataGrid.Items.Refresh();
    }
}
