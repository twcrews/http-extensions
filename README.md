# Crews.Extensions.Http

Provides extension methods and other utilities for working with HTTP resources.

## Installation

```bash
dotnet add package Crews.Extensions.Http
```

## Usage

### `HttpClient` Extensions

#### `SafelySetBaseAddress()`

Safely sets the `HttpClient.BaseAddress` property while avoiding common URI permutation issues.

Now you never have to think about leading or trailing slashes again:

```csharp
var client = new HttpClient();
var baseUri = new Uri("https://api.example.com/v1");

client.SafelySetBaseAddress(baseUri);

var response = await client.GetAsync("users"); 
// Gets https://api.example.com/v1/users
```

### `Uri` Extensions

#### `SetQueryString()`

Set the query string of a `Uri` instance.

```csharp
var uri = new Uri("https://example.com/api/data");
var queryString = new QueryString("?page=1&size=10&tags=important,urgent");

var newUri = uri.SetQueryString(queryString);
// Result: https://example.com/api/data?page=1&size=10&tags=important,urgent
```

> [!IMPORTANT]
> This uses my robust custom `QueryString` structure to avoid the [pitfalls](https://stackoverflow.com/a/76341913) of the built-in `UriBuilder`. But, there's a trade-off: **you are responsible for escaping strings**. This is because it's [impossible](https://stackoverflow.com/a/34189188) for the library to know whether your string is _already_ escaped.

#### `ClearQueryString()`

Remove the query string from a `Uri`:

```csharp
var uri = new Uri("https://example.com/api/data?page=1&size=10");
var cleanUri = uri.ClearQueryString();
// Result: https://example.com/api/data
```

#### `SafelyAppendPath()`

Append paths to `Uri`s without worrying about leading/trailing slashes:

```csharp
var baseUri = new Uri("https://api.example.com/v1/");
var pathUri = baseUri.SafelyAppendPath("users/123");
// Result: https://api.example.com/v1/users/123

// Works regardless of slash configuration
var baseUri2 = new Uri("https://api.example.com/v1");
var pathUri2 = baseUri2.SafelyAppendPath("/users/123");
// Result: https://api.example.com/v1/users/123
```

#### `EnsureTrailingSlash()`

Ensure a `Uri` has one (and only one) trailing slash:

```csharp
var uri1 = new Uri("https://example.com/api").EnsureTrailingSlash();
// Result: https://example.com/api/

var uri2 = new Uri("https://example.com/api///").EnsureTrailingSlash();
// Result: https://example.com/api/
```

### Query String Utilities

This package also contains utility types for efficiently and safely working with query strings. You can even use custom delimiters.

#### `QueryString`

Parse and work with query strings:

```csharp
// Parse a query string
var queryString = new QueryString("?name=John&tags=work,personal&active=true");

// Access parameters
foreach (var param in queryString.Parameters)
{
    Console.WriteLine($"{param.Key}: [{string.Join(", ", param.Values)}]");
}
// Output:
// name: [John]
// tags: [work, personal]
// active: [true]

// Convert back to string
string result = queryString.ToString();
// Result: ?name=John&tags=work,personal&active=true
```

#### `QueryStringBuilder`

Build query strings programmatically:

```csharp
var builder = new QueryStringBuilder();

// Add parameters
builder.Parameters.Add(new QueryString.Parameter 
{ 
    Key = "search", 
    Values = ["user query"] 
});

builder.Parameters.Add(new QueryString.Parameter 
{ 
    Key = "filters", 
    Values = ["active", "verified", "premium"] 
});

// Build the query string
QueryString queryString = builder.QueryString;
string result = queryString.ToString();
// Result: ?search=user query&filters=active,verified,premium
```

#### Custom Delimiters

Customize delimiters for different query string formats:

```csharp
var customQuery = new QueryString("name=John;tags=work:personal;active=true")
{
    BeginningDelimiter = "",
    ParameterDelimiter = ";",
    ParameterValuesDelimiter = ":"
};

// Parse with custom delimiters
foreach (var param in customQuery.Parameters)
{
    Console.WriteLine($"{param.Key}: [{string.Join(", ", param.Values)}]");
}
// Output:
// name: [John]
// tags: [work, personal]
// active: [true]
```

## License

This project is licensed under the GPL-3.0-or-later license.

> S.D.G.
