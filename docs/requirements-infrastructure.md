# Technical & Infrastructure Requirements

This document outlines the technical stack and infrastructure decisions for the FantaTournament project.

## 1. Technical Stack

*   **Runtime**: .NET 8.0
*   **Architecture**: Modular Monolith with Clean Architecture principles.
*   **Projects**:
    *   `FantaTournament.Api`: ASP.NET Core Web API.
    *   `FantaTournament.Application`: Application logic (Commands, Queries, Subscribers).
    *   `FantaTournament.Domain`: Domain entities, value objects, and repository interfaces.
    *   `FantaTournament.Infrastructure`: Implementation of repositories and external services.
    *   `Umbrella.Core`: Core library for common cross-cutting concerns (Messaging, Results, Entities).

## 2. Messaging Infrastructure

The system implements a **Publish-Subscribe** pattern to handle asynchronous side effects (like score recalculations).

*   **Event Bus**: `InMemoryEventBus` (defined in `Umbrella.Core.Messaging`).
*   **Execution**:
    *   Thread-safe and multi-threaded.
    *   Executes all registered subscribers for an event in parallel using `Task.WhenAll`.
    *   Isolated error handling: A failure in one subscriber does not affect others.
*   **Events**: Defined in the Domain layer (e.g., `MatchResultUpdatedEvent`).

## 3. Persistence Strategy

The system uses a file-based persistence strategy for the initial versions to ensure simplicity and portability.

| Entity | Storage Format | Repository Implementation |
| :--- | :--- | :--- |
| **Boards & Teams** | CSV | `CsvBoardRepository`, `CsvTeamRepository` |
| **Forecasts** | JSON | `JsonForecastRepository` |
| **Leagues** | JSON | `JsonLeagueRepository` |
| **Rankings** | JSON | `JsonRankingRepository` |

*   **Data Directory**: Configurable via `appsettings.json` (`Persistence:CsvDataPath`, `Persistence:JsonDataPath`).
*   **Concurrency**: File operations are handled asynchronously. JSON files are stored with unique identifiers (e.g., `FORECAST-{id}.json`).

## 4. Design Principles

*   **Immutability**: Value objects like `Score` and `MatchResult` are records.
*   **Result Pattern**: All application services return a `Result<T>` type to handle success and failure states without throwing exceptions for flow control.
*   **Separation of Concerns**: Recalculation logic is isolated in subscribers (`ForecastRecalculator`, `RankingRecalculator`), triggered by domain events.
