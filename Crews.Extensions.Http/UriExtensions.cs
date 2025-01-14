﻿using Crews.Extensions.Http.Utility;

namespace Crews.Extensions.Http;

/// <summary>
/// Provides extension methods for <see cref="Uri"/> instances.
/// </summary>
public static class UriExtensions
{
  /// <summary>
  /// Appends <paramref name="queryString"/> to <paramref name="uri"/>, replacing any existing query string.
  /// </summary>
  /// <param name="uri">The original <see cref="Uri"/> instance.</param>
  /// <param name="queryString">The <see cref="QueryString"/> instance to append to <paramref name="uri"/>.</param>
  /// <returns>A new <see cref="Uri"/> instance with the appended <see cref="QueryString"/> value.</returns>
  public static Uri SetQueryString(this Uri uri, QueryString queryString)
  {
    UriBuilder uriBuilder = new(uri)
    {
      Query = queryString.ToString()
    };
    return uriBuilder.Uri;
  }

  /// <summary>
  /// Removes the query string from a <see cref="Uri"/> instance.
  /// </summary>
  /// <param name="uri">The original <see cref="Uri"/> instance.</param>
  /// <returns>A new <see cref="Uri"/> instance with its query string removed.</returns>
  public static Uri ClearQueryString(this Uri uri)
  {
    UriBuilder uriBuilder = new(uri)
    {
      Query = string.Empty
    };
    return uriBuilder.Uri;
  }

  /// <summary>
  /// Appends the given <paramref name="path"/> to the <paramref name="uri"/>.
  /// </summary>
  /// <param name="uri">The original URI instance.</param>
  /// <param name="path">The path to append.</param>
  /// <returns>A new URI instance with the <paramref name="path"/> appended.</returns>
  /// <remarks>
  /// The <see cref="Uri"/> class provides a helpful constructor for appending paths: <c>new Uri(Uri, string)</c>. 
  /// Unfortunately, this constructor can produce siginificantly variable results depending on the value of its
  /// parameters; specifically, the presence of leading and trailing slashes can cause parts of the URI path to be lost.
  /// </remarks>
  public static Uri SafelyAppendPath(this Uri uri, string path)
  {
    string uriString = uri.OriginalString;
    uriString = uriString.TrimEnd('/');
    path = path.TrimStart('/');
    return new($"{uriString}/{path}");
  }

  /// <summary>
  /// Adds a trailing slash to <paramref name="uri"/> if one does not already exists, and trims any redundant trailing 
  /// slashes.
  /// </summary>
  /// <param name="uri">The original <see cref="Uri"/> instance.</param>
  /// <returns>A new <see cref="Uri"/> with exactly one trailing slash.</returns>
  public static Uri EnsureTrailingSlash(this Uri uri) => new($"{uri.OriginalString.TrimEnd('/')}/");
}
