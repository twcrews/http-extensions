using Crews.Extensions.Primitives;

namespace Crews.Extensions.Http;

/// <summary>
/// Represents the query string at the end of a URI.
/// </summary>
public readonly record struct QueryString
{
  private readonly string _queryString;

  /// <summary>
  /// A collection of the parameters in the query string.
  /// </summary>
  public IEnumerable<Parameter> Parameters
  {
    get
    {
      string parameterAssignmentDelimiter = ParameterAssignmentDelimiter;
      string parameterValuesDelimiter = ParameterValuesDelimiter;

      return _queryString
      .TrimStart(BeginningDelimiter)
      .TrimEnd(EndingDelimiter)
      .Split(ParameterDelimiter, StringSplitOptions.RemoveEmptyEntries)
      .Select(parameterString =>
      {
        string[] parameterParts = parameterString.Split(parameterAssignmentDelimiter);
        if (parameterParts.Length > 2)
        {
          throw new FormatException("A query string parameter contains multiple assignment delimiters");
        }
        if (string.IsNullOrEmpty(parameterParts[0]))
        {
          throw new FormatException("A query string parameter name is empty");
        }
        return new Parameter(
          parameterParts[0],
          parameterParts.Length == 2 ? [.. parameterParts[1].Split(parameterValuesDelimiter)] : []);
      });
    }
  }

  /// <summary>
  /// The string at the start of the query string. Defaults to '?'.
  /// </summary>
  public string BeginningDelimiter { get; init; } = "?";

  /// <summary>
  /// The string at the end of the query string. Defaults to an empty string.
  /// </summary>
  public string EndingDelimiter { get; init; } = string.Empty;

  /// <summary>
  /// The string used to divide parameters. Defaults to '&amp;'.
  /// </summary>
  public string ParameterDelimiter { get; init; } = "&";

  /// <summary>
  /// The string used to divide parameter keys from their values. Defaults to '='.
  /// </summary>
  public string ParameterAssignmentDelimiter { get; init; } = "=";

  /// <summary>
  /// The string used to divide values in an array-style parameter assignment. Defaults to ','.
  /// </summary>
  public string ParameterValuesDelimiter { get; init; } = ",";

  /// <summary>
  /// Parses a string into a QueryString object. 
  /// Delimiters can be changed using the object initializer.
  /// The order of the string will be preserved when parsing and serializing via ToString(). 
  /// </summary>
  /// <param name="queryString">The string to be parsed.</param>
  public QueryString(string queryString)
  {
    if (string.IsNullOrWhiteSpace(queryString))
    {
      throw new FormatException("Query string was null, empty, or consisted only of whitespace characters.");
    }

    _queryString = queryString.Trim();
  }

  /// <inheritdoc />
  public override string ToString()
  {
    string parameterAssignmentDelimiter = ParameterAssignmentDelimiter;
    string parameterValuesDelimiter = ParameterValuesDelimiter;

    return BeginningDelimiter +
      string.Join(ParameterDelimiter, Parameters.Select(p =>
        p.Key +
        parameterAssignmentDelimiter +
        string.Join(parameterValuesDelimiter, p.Values))) +
        EndingDelimiter;
  }

  /// <summary>
  /// Represents a parameter of a URI query string.
  /// </summary>.
  /// <param name="Key">The key (name) of the parameter.</param>
  /// <param name="Values">
  /// The values assigned to the parameter. The order of these values will be preserved.
  /// </param>
  public record class Parameter(string Key, IReadOnlyList<string> Values)
  {
    /// <summary>
    /// Convenience constructor which allows the use of <see langword="params" /> for values.
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="Values"></param>
    public Parameter(string Key, params string[] Values) : this(Key, (IReadOnlyList<string>)[.. Values]) { }
  }
}
