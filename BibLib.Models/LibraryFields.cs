namespace BibLib.Models;

public class LibraryFields
{
    public IList<string> Article { get; set; } =
    [
        "Title",
        "Author",
        "Journal",
        "Year",
        "Keywords",
        "CreateTime",
        "UpdateTime"
    ];

    public IList<string> Book { get; set; } =
    [
        "Title",
        "Author",
        "Year",
        "Publisher",
        "Keywords",
        "CreateTime",
        "UpdateTime"
    ];
}
