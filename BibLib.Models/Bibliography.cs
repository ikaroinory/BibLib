using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibLib.Models;

public abstract class Bibliography(DateTime createTime)
{
    [Key] public Guid Guid { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = "";
    public DateTime CreateTime { get; set; } = createTime;
    public DateTime? UpdateTime { get; set; }

    public string? FilePath { get; set; }

    [NotMapped] public bool IsSelected { get; set; } = false;

    protected Bibliography() : this(DateTime.Now) { }
}
