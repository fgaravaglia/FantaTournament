# FantaTournament - Requirements & Project Documentation

FantaTournament is a platform that manages the forecasts on the results of the football matches of the FIFA World Cup or UEFA European Championship.

Below you find the detaile requirements:
- **[Functional Requirements](./requirements-functional.md)** - What and How System is suppose to do and how
- **[Asynchronous Processing & Events](./requirements-asyncprocessing.md)** - Implemented via a **Publish-Subscriber** pattern (Event Bus) to handle side effects like scoring and rankings in parallel.
- **[REST API & Exposed Capabilities](./03-Implementation-strategy/application-layer.md)** - Exposure of the application layer via ASP.NET Core Controllers.
- **[Authentication & Security (Auth0)](./03-Implementation-strategy/auth0.md)** - Identity management and JWT validation using Auth0.
- **[Technical & Infrastructure Requirements](./requirements-infrastructure.md)** - The non-functional requiremets
