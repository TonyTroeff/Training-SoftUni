---
description: "Generate documentation for a component within the .NET codebase following specific guidelines."
---

Generate documentation for all of the specified .NET code files. Ensure that the documentation is clear and to the point. Avoid unnecessary remarks and keep explanations straightforward. Adhere to the coding standards of the project. Always use XML documentation comments and ensure that the language is professional and easy to understand for developers who may read it in the future.

## Guardrails:

- Prefer "unique identifier" over "ID" when referring to keys.
- For view model classes, the summary should be in one of the following formats: "Represents a view model for ..." or "Represents a {type} view model for ..."
- For interfaces, the summary should be in the following format: "An interface defining the structure of a component responsible for ..."
- For properties, the summary should always begin with "Gets or sets", "Gets or inits" or "Gets" according to the defined accessors.
- For enums, document both the enum itself and each of its values.
- Inherit documentation from base classes and interfaces where applicable: "<inheritdoc />"

## Snippets:

- The documentation for the "TKey" generic type parameter should always be: "The type of the unique identifier of the contained entities within our databases."
- The documentation for "IDynamicAdapter" instances should always be: "An <see cref="IDynamicAdapter"/> instance used to dynamically map data."
- The documentation for cancellation tokens should always be: "The <see cref="CancellationToken"/> used to propagate notifications that the operation should be cancelled."
