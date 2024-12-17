using BibLib.Models;
using Microsoft.EntityFrameworkCore;

namespace BibLib.ViewModels;

public class ApplicationDatabase : DbContext
{
    public DbSet<ArticleBibliography> Articles { get; set; }
    public DbSet<BookBibliography> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite($"Data Source={GlobalResources.DatabaseFilePath}");
}
