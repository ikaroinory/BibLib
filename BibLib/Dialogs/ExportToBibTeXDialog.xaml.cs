using System.Windows;

namespace BibLib.Dialogs;

public partial class ExportToBibTeXDialog : Window
{
    public ExportToBibTeXDialog() => InitializeComponent();

    private void CopyButton_OnClick(object sender, RoutedEventArgs e) => Clipboard.SetText(BibTexTextBox.Text);
}
