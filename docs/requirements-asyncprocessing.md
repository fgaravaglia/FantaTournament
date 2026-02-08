# Asynchronous Processing & Events

The system must handle heavy computations (like scoring and rankings) asynchronously to ensure high responsiveness and scalability.

## 1. Match Result Update Event

Whenever a match result is updated (Status marked as `Played`), the system must publish a "Match Result Updated" event.

## 2. Parallel Subscribers

Multiple subscribers must react to the match result event in parallel using a **Publish-Subscriber** pattern:

1.  **Forecast Recalculator**:
    *   Must identify and ricalculate all forecasts associated with the Board of the match.
    *   Each forecast recalculation should ideally occur in parallel/asynchronously.

2.  **Ranking Recalculator**:
    *   Must recalulate all League rankings (and the Global ranking) for the associated Board.
    *   Processing for each league can be performed in parallel.

## 3. Execution Requirements

*   **Asynchronous**: The API call to update the match result should return immediately after publishing the event, without waiting for recalculations.
*   **Multithreaded**: Recalculations for multiple forecasts/leagues should leverage multithreading to minimize total processing time.
*   **Decoupled**: The Board service should not know about Forecast or Ranking scoring logic; it only publishes the event.

## 4. Technical Implementation

The system utilizes the `InMemoryEventBus` from `Umbrella.Core.Messaging`:

*   **Registration**: Subscribers (handlers) are registered in the Dependency Injection container as `IEventHandler<T>`.
*   **Parallelism**: Upon publication, the bus resolves all handlers and executes them concurrently using `Task.WhenAll`.
*   **Resilience**: Exceptions within a subscriber are caught and logged, allowing other subscribers to finish their work undisturbed.
