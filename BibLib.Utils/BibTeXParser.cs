using System.Reflection;
using BibLib.Models;

namespace BibLib.Utils;

public class BibTeXParser
{
    private int _index;
    private readonly string _bibString;
    private readonly List<Bibliography> _bibList;

    public BibTeXParser(string bibString)
    {
        _index = 0;
        _bibString = bibString;
        _bibList = [];
    }

    private void SkipWhitespaceAndNewLines()
    {
        while (_index < _bibString.Length && _bibString[_index] is ' ' or '\n' or '\r' or '\t') _index++;
    }

    private static bool IsLegalTypeCharacter(char c) => char.IsLetterOrDigit(c) || c == '_';

    private static void ProcessingProperties(IDictionary<string, string> properties)
    {
        foreach (var key in properties.Keys)
        {
            if (!string.Equals(key, "author", StringComparison.CurrentCultureIgnoreCase)) continue;

            if (!properties[key].Contains("and")) continue;

            var strings = properties[key].Split("and").Select(s => s.Trim());
            properties[key] = string.Join(", ", strings);
        }
    }

    private static Bibliography ParseBibliography(string typeName, IDictionary<string, string> properties)
    {
        ProcessingProperties(properties);

        Bibliography bibliography = typeName switch
        {
            "article" => new ArticleBibliography(),
            "book" => new BookBibliography(),
            _ => throw new FormatException("Invalid BibTeX string.")
        };

        var propertiesArray = bibliography.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

        properties.ToList().ForEach(pair =>
        {
            var property = propertiesArray.FirstOrDefault(
                prop => string.Equals(prop.Name, pair.Key, StringComparison.CurrentCultureIgnoreCase)
            );

            property?.SetValue(bibliography,
                property.PropertyType == typeof(int) || property.PropertyType == typeof(int?)
                    ? int.Parse(pair.Value)
                    : pair.Value
            );
        });

        return bibliography;
    }

    private string ParseTypeName()
    {
        var type = "";
        while (_index < _bibString.Length && _bibString[_index] is not '{')
        {
            if (!IsLegalTypeCharacter(_bibString[_index])) throw new FormatException("Invalid BibTeX string.");
            type += _bibString[_index];
            _index++;
        }

        return type;
    }

    private string ParseLabel()
    {
        var label = "";
        while (_index < _bibString.Length && _bibString[_index] is not ',')
        {
            if (!IsLegalTypeCharacter(_bibString[_index])) throw new FormatException("Invalid BibTeX string.");
            label += _bibString[_index];
            _index++;
        }

        return label;
    }

    private string ParsePropertyName()
    {
        var propertyName = "";
        while (_bibString[_index] is not '=' and not ' ')
        {
            if (!IsLegalTypeCharacter(_bibString[_index])) throw new FormatException("Invalid BibTeX string.");
            propertyName += _bibString[_index];
            _index++;
        }

        return propertyName;
    }

    private string ParsePropertyValue()
    {
        if (_bibString[_index] is not '{') throw new FormatException("Invalid BibTeX string.");

        _index++; // Skip '{'


        var propertyValue = "";
        while (_index < _bibString.Length && _bibString[_index] is not '}')
        {
            propertyValue += _bibString[_index];
            _index++;
        }

        _index++; // Skip '}'

        return propertyValue.Trim();
    }

    public List<Bibliography> Parse()
    {
        while (_index < _bibString.Length)
        {
            SkipWhitespaceAndNewLines();

            if (_bibString[_index] is not '@') throw new FormatException("Invalid BibTeX string.");

            _index++; // Skip '@'

            var typeName = ParseTypeName();

            _index++; // Skip '{'

            // Empty Bibliography
            if (_bibString[_index] is '}')
            {
                _index++;
                continue;
            }

            SkipWhitespaceAndNewLines();

            _ = ParseLabel();

            _index++; // Skip ','

            SkipWhitespaceAndNewLines();

            // Parse properties.
            var properties = new Dictionary<string, string>();
            while (true)
            {
                var propertyName = ParsePropertyName();

                SkipWhitespaceAndNewLines();
                _index++; // Skip '='
                SkipWhitespaceAndNewLines();
                var propertyValue = ParsePropertyValue();
                SkipWhitespaceAndNewLines();

                if (_bibString[_index] is not ',') break;

                _index++; // Skip ','

                properties[propertyName] = propertyValue;

                SkipWhitespaceAndNewLines();
            }

            SkipWhitespaceAndNewLines();
            if (_bibString[_index] is not '}') throw new FormatException("Invalid BibTeX string.");

            _index++; // Skip '}'

            try
            {
                var bibliography = ParseBibliography(typeName, properties);
                _bibList.Add(bibliography);
            }
            catch (FormatException) { }

            SkipWhitespaceAndNewLines();
        }

        return _bibList;
    }
}
