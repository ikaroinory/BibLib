using System.ComponentModel.DataAnnotations.Schema;

namespace BibLib.Models;

[Table("Books")]
public class BookBibliography : Bibliography
{
    public string? Author { get; set; }
    public int? Year { get; set; }
    public string? Publisher { get; set; }
    public string? City { get; set; }
    public string? Abstract { get; set; }
    public string? Language { get; set; }

    public string? Url { get; set; }
    public int? Volume { get; set; }
    public string? Pages { get; set; }
    public string? Doi { get; set; }
    public string? Isbn { get; set; }
    public string? Keywords { get; set; }

    public BookBibliography() { }
    public BookBibliography(DateTime createTime) : base(createTime) { }
}
