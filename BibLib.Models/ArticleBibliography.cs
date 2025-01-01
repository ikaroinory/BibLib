using System.ComponentModel.DataAnnotations.Schema;

namespace BibLib.Models;

[Table("Articles")]
public class ArticleBibliography : Bibliography
{
    public string? Journal { get; set; }
    public string? Volume { get; set; }
    public int? Issue { get; set; }
    public string? Pages { get; set; }
    public string? Abstract { get; set; }
    public string? Language { get; set; }
    public string? Publisher { get; set; }

    public string? Url { get; set; }
    public string? Doi { get; set; }
    public string? Issn { get; set; }
    public string? Keywords { get; set; }

    public ArticleBibliography() { }
    public ArticleBibliography(DateTime createTime) : base(createTime) { }
}
