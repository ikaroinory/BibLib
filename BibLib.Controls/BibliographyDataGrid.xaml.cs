using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BibLib.Controls;

public partial class BibliographyDataGrid : UserControl
{
    public static readonly DependencyProperty FieldsProperty = DependencyProperty.Register(
        nameof(Fields),
        typeof(List<ColumnProperty>),
        typeof(BibliographyDataGrid),
        new PropertyMetadata(default, Fields_OnChanged)
    );

    public static readonly DependencyProperty BibliographiesProperty = DependencyProperty.Register(
        nameof(Bibliographies),
        typeof(IEnumerable<object>),
        typeof(BibliographyDataGrid),
        new PropertyMetadata(default, Bibliographies_OnChanged)
    );

    public List<ColumnProperty> Fields
    {
        get => (List<ColumnProperty>)GetValue(FieldsProperty);
        set => SetValue(FieldsProperty, value);
    }

    public IEnumerable<object> Bibliographies
    {
        get => (IEnumerable<object>)GetValue(BibliographiesProperty);
        set => SetValue(BibliographiesProperty, value);
    }

    public BibliographyDataGrid() => InitializeComponent();

    private static void Fields_OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not BibliographyDataGrid control || e.NewValue is not List<ColumnProperty> newFields) return;
        control.GenerateColumns(newFields);
    }

    private static void Bibliographies_OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not BibliographyDataGrid control) return;
        control.DataGrid.ItemsSource = (IEnumerable<object>)e.NewValue;
        control.DataGrid.Items.Refresh();
    }

    private void DataGrid_OnLoadingRow(object? sender, DataGridRowEventArgs e) => e.Row.Header = (e.Row.GetIndex() + 1).ToString();

    private void GenerateColumns(List<ColumnProperty> fields)
    {
        DataGrid.Columns.Clear();

        DataGrid.Columns.Add(new DataGridCheckBoxColumn
        {
            Header = "Select",
            Binding = new Binding("IsSelected")
        });
        fields.ForEach(field =>
        {
            DataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = field.Name,
                Binding = new Binding(field.Name),
                Width = new DataGridLength(field.Width)
            });
        });
    }
}
