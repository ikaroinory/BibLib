using System.Collections.ObjectModel;
using BibLib.Models;
using BibLib.Utils;

namespace BibLib.ViewModels;

public class LibraryViewModel : BaseViewModel
{
    private ObservableCollection<ArticleBibliography> _articles = [];
    private ObservableCollection<BookBibliography> _books = [];
    private ObservableCollection<string> _articleFields = [];
    private ObservableCollection<string> _bookFields = [];

    public ObservableCollection<ArticleBibliography> Articles
    {
        get => _articles;
        set => Set(ref _articles, value);
    }

    public ObservableCollection<BookBibliography> Books
    {
        get => _books;
        set => Set(ref _books, value);
    }

    public ObservableCollection<string> ArticleFields
    {
        get => _articleFields;
        set => Set(ref _articleFields, value);
    }

    public ObservableCollection<string> BookFields
    {
        get => _bookFields;
        set => Set(ref _bookFields, value);
    }


    public LibraryViewModel()
    {
        RefreshBibliographies();

        foreach (var field in GlobalResources.Configuration.LibraryFields.Article)
        {
            ArticleFields.Add(field);
        }

        foreach (var field in GlobalResources.Configuration.LibraryFields.Book)
        {
            BookFields.Add(field);
        }
    }

    public void RefreshBibliographies()
    {
        using var database = new ApplicationDatabase();

        _articles.Clear();
        database.Articles.ToList().ForEach(Articles.Add);

        _books.Clear();
        database.Books.ToList().ForEach(Books.Add);
    }

    public IDictionary<string, int> AddRange(IEnumerable<Bibliography> bibliographies)
    {
        var countDictionary = new Dictionary<string, int>();
        var list = bibliographies.ToList();

        using var database = new ApplicationDatabase();

        database.Articles.AddRange(list.OfType<ArticleBibliography>());
        countDictionary["article"] = database.SaveChanges();

        database.Books.AddRange(list.OfType<BookBibliography>());
        countDictionary["book"] = database.SaveChanges();

        return countDictionary;
    }

    public IDictionary<string, int> RemoveRange()
    {
        var countDictionary = new Dictionary<string, int>();

        using var database = new ApplicationDatabase();

        database.Articles.RemoveRange(_articles.ToList().Where(bib => bib.IsSelected));
        countDictionary["article"] = database.SaveChanges();

        database.Books.RemoveRange(_books.ToList().Where(bib => bib.IsSelected));
        countDictionary["book"] = database.SaveChanges();

        return countDictionary;
    }

    public string Export()
    {
        List<string> list = [];
        list.AddRange(_articles.ToList().Where(bib => bib.IsSelected).Select(bib => bib.ToBibTeXString()));
        list.AddRange(_books.ToList().Where(bib => bib.IsSelected).Select(bib => bib.ToBibTeXString()));
        var str = string.Join(Environment.NewLine + Environment.NewLine, list);
        return str == string.Empty ? "" : str + Environment.NewLine;
    }
}
