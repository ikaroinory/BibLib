namespace BibLib.Models;

public class LibraryFields
{
    public IList<string> Article { get; set; } =
    [
        "Title",
        "Author",
        "Year",
        "Keywords",
        "Create Time"
    ];

    public IList<string> Book { get; set; } =
    [
        "Title",
        "Author",
        "Year",
        "Publisher",
        "Keywords",
        "Create Time"
    ];
}
