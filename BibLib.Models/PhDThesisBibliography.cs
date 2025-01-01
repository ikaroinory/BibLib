using System.ComponentModel.DataAnnotations.Schema;

namespace BibLib.Models;

[Table("PhDTheses")]
public class PhDThesisBibliography : Bibliography
{
    public string School { get; set; } = string.Empty;
    
    public string? Address { get; set; }
    public string? Keywords { get; set; }
}
