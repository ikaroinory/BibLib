using System.ComponentModel.DataAnnotations.Schema;

namespace BibLib.Models;

[Table("MastersTheses")]
public class MastersThesisBibliography : Bibliography
{
    public string Author { get; set; } = string.Empty;
    public string School { get; set; } = string.Empty;

    public string? Type { get; set; }
    public string? Address { get; set; }
}
