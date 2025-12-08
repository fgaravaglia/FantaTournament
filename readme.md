# Repository Description

This repository contains the source fode of "FantaTournament", a web site to play with FIFA World Cup or UEFA Euro Cup.
the goal is playing with match forecast and guess the best results of each matches of the tournament.

## Technology Stack

- **Backend**: dotnet
- **Frontend**: dotnet MVC
- **Database**: Google Firestore
- **Deployment**: Azure Devops
- **Infrastructure**: Terraform

## Repository Structure

The project strictly follows **Onion Architecture** patterns with the core principle being the **Dependency Rule**. This is the the source code structure:

```txt
Repository/
|-- docs/
|-- IaC/
|-- scripts/
|-- src/
|   |-- FantaTournament.Application/
|   |   |-- FantaTournament.Application.csproj
|   |-- FantaTournament.Domain/
|   |   |-- FantaTournament.Domain.csproj
|   |-- FantaTournament.Infrastructure/
|   |   |-- FantaTournament.Infrastructure.csproj
|   |-- Umbrella.Core/
|   |   |-- Umbrella.Core.csproj
```

it is divided into documentation (docs folder), script (scripts folder) and Source code (src folder).

### Docs Folder

it contains the generic documentation of the project like Gemini.md files to configure the AI code developer agents.

- **Gemini.md Files Hierarchy** - for more details, see:  [https://github.com/google-gemini/gemini-cli/blob/main/docs/cli/configuration.md#context-files-hierarchical-instructional-context]
- **Gemini CLi Integration with VS Code** - see more details here:  [https://github.com/google-gemini/gemini-cli/blob/main/docs/ide-integration.md]

### IaC Folder

This folder contains all scripts needed to deploy the artifact like REST API to target environment using Terraform.

### Scripts Folder

this folder contains all required powershell scripts that are useful for the repository.

### Src Folder

inside src folder, the code is organized in the following way:

- Domain / Application  / Infrastructure: it contains the business logic (aka: the domain library), the application layer and infrastructure library for the specific-domain-related concepts, like persistence.
- COre: it contains shared objects and capabilities