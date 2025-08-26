# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a .NET 8 library that provides extension methods and utilities for working with HTTP resources. The project consists of two main components:

- **Crews.Extensions.Http**: The main library containing extension methods for HttpClient and Uri, plus query string utilities
- **Crews.Extensions.Http.Tests**: XUnit test project with comprehensive test coverage

## Development Commands

### Building
```bash
dotnet build
dotnet build --configuration Release
```

### Testing  
```bash
dotnet test
dotnet test --collect:"XPlat Code Coverage"
```

### Running a single test
```bash
dotnet test --filter "MethodName"
dotnet test --filter "ClassName"
```

### Packaging
```bash
dotnet pack --configuration Release
```

## Architecture

### Core Components

**HttpClientExtensions** (`HttpClientExtensions.cs`): Contains `SafelySetBaseAddress()` method that safely sets HttpClient.BaseAddress while avoiding URI permutation side effects by ensuring trailing slashes.

**UriExtensions** (`UriExtensions.cs`): Provides methods for URI manipulation:
- `SetQueryString()` - Replaces query string with QueryString instance
- `ClearQueryString()` - Removes query string entirely  
- `SafelyAppendPath()` - Safely appends paths avoiding slash-related URI issues
- `EnsureTrailingSlash()` - Ensures exactly one trailing slash

**QueryString Utilities** (`Utility/`):
- `QueryString` readonly record struct: Value type query string parser with configurable delimiters
- `QueryStringBuilder` class: Mutable builder for constructing query strings

### Key Design Patterns

- **QueryString as readonly record struct**: The QueryString is now a value type that parses once on construction and provides read-only access to parameters
- **Safe URI Operations**: Extension methods specifically address common URI manipulation pitfalls with slashes and path concatenation
- **Configurable Delimiters**: Both QueryString classes support custom delimiters for different query string formats

### Dependencies

- **Crews.Extensions.Primitives**: External dependency for primitive extensions
- **XUnit**: Testing framework with Visual Studio runner integration
- **Coverlet**: Code coverage collection

### Project Structure

- Main library code in `Crews.Extensions.Http/` namespace
- Utility classes in `Crews.Extensions.Http.Utility/` namespace  
- Corresponding test files mirror the main library structure in test project