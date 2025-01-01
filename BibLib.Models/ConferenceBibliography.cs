using System.ComponentModel.DataAnnotations.Schema;

namespace BibLib.Models;

[Table("Conferences")]
public class ConferenceBibliography : Bibliography
{
    public string BookTitle { get; set; } = string.Empty;

    public string? Editor { get; set; }
    public int? Volume { get; set; }
    public string? Series { get; set; }
    public string? Pages { get; set; }
    public string? Address { get; set; }
    public string? Organization { get; set; }
    public string? Publisher { get; set; }

    public ConferenceBibliography() { }
    public ConferenceBibliography(DateTime createTime) : base(createTime) { }
}
