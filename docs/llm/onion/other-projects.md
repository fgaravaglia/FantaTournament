# Other projects

this document explains the meaning of other porjects found in this repository.

## Umbrella.Mapper

The `Umbrella.Mapper` project provides a foundational, reflection-based object mapping mechanism
for .NET applications within the Umbrella architecture. Its primary purpose is to facilitate the transfer of data between objects of different types, commonly used for converting domain entities to Data Transfer Objects (DTOs) or vice-versa.

## Umbrella.Core

`Umbrella.Core` is a foundational project that provides the essential building blocks for implementing a Domain-Driven Design (DDD) approach within the Umbrella architecture. It contains a set of reusable interfaces, base classes, and helper types that establish a common language and structure for the domain layer of an application. Its primary goal is to enforce DDD principles and provide robust patterns for creating a clean, scalable, and maintainable domain model.

### 1. Result Pattern

* **`Result<T>`**: A generic class used to encapsulate the outcome of an operation. Instead of throwing exceptions for business rule failures, methods can return a `Result` object.
  * It indicates whether an operation `Succeeded` or failed.
  * On success, it can carry a `Data` payload of type `T`.
  * On failure, it contains a collection of `Errors` (strings) explaining what went wrong.
  * Includes static factory methods like `Success(T data)`, `Failure(errors)`, and `NotFound()` to simplify its creation.
    This pattern promotes a more explicit and predictable way of handling operation outcomes.

### 2. Domain-Driven Design (DDD) Building Blocks

* **`IEntity` and `Entity`**:
  * `IEntity` defines the most basic contract for a domain entity: a unique `Id` (string).
  * `Entity` is an abstract base class implementing `IEntity`, automatically assigning a `Guid` as the `Id`. This provides a convenient starting point for creating domain entities.

* **`IAuditableEntity`**:
  * Extends `IEntity` to add properties for auditing purposes.
  * It tracks creation and modification details: `CreatedBy`, `CreatedDate`, `UpdatedBy`, and `UpdatedDate`. This is useful for entities where a history of changes is required.

* **`ValueObject`**:
  * An abstract base class for creating Value Objects, a core DDD pattern for objects defined by their attributes rather than a unique identity.
  * It provides built-in structural equality. Two `ValueObject` instances are considered equal if all their constituent components (defined in `GetEqualityComponents()`) are equal. The class overrides `Equals()` and `GetHashCode()` to enforce this behavior.

* **`IKeyValuePairObject`**:
  * An interface for a specific kind of `ValueObject` that represents a simple key-value pair (`Code`, `Value`).

## Purpose and Usage

By providing these core types, `Umbrella.Core` establishes a solid foundation for the entire solution. It ensures that all domain entities and value objects adhere to a consistent structure and behavior. Using the `Result<T>` class across the application standardizes error handling and improves the clarity of the codebase. This project is the essential bedrock upon which the application's domain logic is built.
