using System.Windows;

namespace BibLib.Dialogs;

public partial class AddBibliographyFromBibTeXDialog : Window
{
    public string BibTexText { get; private set; } = "";

    public AddBibliographyFromBibTeXDialog() => InitializeComponent();

    private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
    {
        BibTexText = BibTexTextBox.Text;

        if (string.IsNullOrEmpty(BibTexText))
        {
            MessageBox.Show("Please enter the content of the BibTeX entry!", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        DialogResult = true;
        Close();
    }
}
