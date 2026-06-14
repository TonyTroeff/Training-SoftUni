---
name: dotnet-collection-best-practices
description: Best practices for collection handling in .NET. Load when creating/initializing collections, choosing collection types for properties/fields/return types, designing public APIs with collections, or modernizing collection syntax.
---

# .NET Collection Best Practices

## 1. Use Collection Expressions with Type Inference

Use collection expressions `[]` when the type can be inferred:

```csharp
// ✅ GOOD
List<string> names = ["Alice", "Bob", "Charlie"];
IImmutableList<int> values = [1, 2, 3, 4, 5];
var links = item.Links ?? [];

// ❌ AVOID
List<string> names = new List<string> { "Alice", "Bob", "Charlie" };
IImmutableList<int> values = ImmutableArray.Create(1, 2, 3, 4, 5);
var links = item.Links ?? Enumerable.Empty<Link>();
```

Use spread operator `..` to combine collections:

```csharp
IImmutableList<string> combined = [.. collection1, .. collection2, "extra"];
```

## 2. Prefer Immutable Collections

Use `IImmutableList<T>` instead of `IReadOnlyCollection<T>`:

```csharp
// ✅ GOOD
public required IImmutableList<string> Permissions { get; init; }
public IImmutableList<Customer> GetActiveCustomers() => this._customers.Where(c => c.IsActive).ToImmutableList();

// ❌ AVOID
public IReadOnlyCollection<Customer> GetActiveCustomers() => this._customers.Where(c => c.IsActive).ToList().AsReadOnly();
```

## Key Patterns

```csharp
// Empty collections
IImmutableList<string> empty = [];

// Properties
public class Document
{
    // Optional property with default (empty) value
    public IImmutableList<string> Tags { get; init; } = [];

    // Required property
    public required IImmutableList<string> Permissions { get; init; }
}
```

## When to Use Alternatives

- **List<T>**: Private/internal collections that require mutability and are not exposed as part of the public API
- **Arrays**: External API interop, performance-critical with fixed size, spans
