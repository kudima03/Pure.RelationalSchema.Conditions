# Pure.RelationalSchema.Conditions

`IBool` condition types over relational schema components for the **Pure** ecosystem.

[![.NET build & test](https://github.com/kudima03/Pure.RelationalSchema.Conditions/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.RelationalSchema.Conditions/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.RelationalSchema.Conditions/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.RelationalSchema.Conditions/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.RelationalSchema.Conditions)](https://www.nuget.org/packages/Pure.RelationalSchema.Conditions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`Pure.RelationalSchema.Conditions` provides composable `IBool` condition types that evaluate structural predicates over relational schema objects — schemas, tables, columns, indexes, and foreign keys. Each condition is a lazy, immutable value object that implements `IBool` from `Pure.Primitives.Abstractions`.

## Design Principles

- **IBool results** — every condition is an `IBool`, keeping results composable within the Pure type system.
- **Lazy evaluation** — conditions are evaluated only when `.BoolValue` is accessed.
- **AOT-compatible** — no reflection; fully compatible with Native AOT compilation.

## Dependencies

- [`Pure.RelationalSchema.Abstractions`](https://github.com/kudima03/Pure.RelationalSchema.Abstractions) — relational schema domain interfaces
- [`Pure.Primitives.Abstractions`](https://github.com/kudima03/Pure.Primitives.Abstractions) — `IBool` interface
