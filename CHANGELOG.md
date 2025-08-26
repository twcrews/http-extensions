# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0] - 2025-08-25

### Added

- Add usage instructions to the README.

### Changed
- **Breaking Change**: Removed `Crews.Extensions.Http.Utility` namespace. All types have been moved to `Crews.Extensions.Http` for simplicity.
- Convert `QueryString` into a `record struct` for better performance and value semantics.
- Upgrade `Crews.Extensions.Primitives` dependency to version 1.1.2.

## [1.0.0] - 2024-11-29

Initial release.

[2.0.0]: https://github.com/twcrews/http-extensions/compare/1.0.0...2.0.0
[1.0.0]: https://github.com/twcrews/http-extensions/releases/tag/1.0.0
