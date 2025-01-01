using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibLib.Models;

public abstract class Bibliography(DateTime createTime)
{
    [Column(Order = 0)]
    [Key]
    public Guid Guid { get; set; } = Guid.NewGuid();

    [Column(Order = 1)]
    public string Title { get; set; } = string.Empty;

    [Column(Order = 2)]
    public int Year { get; set; }

    public int? Month { get; set; }
    public string? Note { get; set; }

    public DateTime CreateTime { get; set; } = createTime;
    public DateTime? UpdateTime { get; set; }
    public string? FilePath { get; set; }

    [NotMapped]
    public bool IsSelected { get; set; }

    protected Bibliography() : this(DateTime.Now) { }
}
