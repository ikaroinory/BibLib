using System.Windows;

namespace BibLib.Dialogs;

public partial class AddBibliographyDialog : Window
{
    public string BibTexText { get; private set; } = "";

    public AddBibliographyDialog() => InitializeComponent();
}
