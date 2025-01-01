using System.ComponentModel.DataAnnotations.Schema;

namespace BibLib.Models;

[Table("Proceedings")]
public class ProceedingsBibliography : Bibliography
{
    public string? Editor { get; set; }
    public int? Volume { get; set; }
    public string? Series { get; set; }
    public string? Address { get; set; }
    public string? Organization { get; set; }
    public string? Publisher { get; set; }
}
