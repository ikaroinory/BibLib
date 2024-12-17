using BibLib.Models;

namespace BibLib.Utils;

public static class BibTeXExtension
{
    private static string ToBibTeXString(this ArticleBibliography bibliography)
    {
        return $"""
                @article {'{'}
                    author = {'{'}{bibliography.Author}{'}'},
                    title = {'{'}{bibliography.Title}{'}'},
                    journal = {'{'}{bibliography.Journal}{'}'},
                    volume = {'{'}{bibliography.Volume}{'}'},
                    pages = {'{'}{bibliography.Pages}{'}'},
                    year = {'{'}{bibliography.Year}{'}'},
                    issn = {'{'}{bibliography.Issn}{'}'}
                {'}'}
                """;
    }

    private static string ToBibTeXString(this BookBibliography bibliography)
    {
        return $"""
                @book {'{'}
                    author    = {'{'}{bibliography.Author}{'}'},
                    title     = {'{'}{bibliography.Title}{'}'},
                    publisher = {'{'}{bibliography.Publisher}{'}'},
                    year      = {'{'}{bibliography.Year}{'}'}
                {'}'}
                """;
    }

    public static string ToBibTeXString(this Bibliography bibliography)
    {
        if (bibliography.GetType() == typeof(ArticleBibliography))
            return ((ArticleBibliography)bibliography).ToBibTeXString();
        if (bibliography.GetType() == typeof(BookBibliography))
            return ((BookBibliography)bibliography).ToBibTeXString();

        throw new FormatException("Invalid BibTeX type.");
    }
}
