namespace BibLib.Models;

public class Configuration
{
    public string Language { get; set; } = "en_us";
    public LibraryFields LibraryFields { get; set; } = new();
}
