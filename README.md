# Repository Description

Put here the description of the repository

## Technology Stack

- **Backend**: dotnet REST API
- **Frontend**: [REACT js  + Typescript]  

this repository has the following structure:

```txt
Repository/
|-- docs/
|-- IaC/
|-- scripts/
|-- src/
|   |-- FantaTournament.Application/                            
|   |   |-- FantaTournament.Application.csproj                # Application Layer Project
|   |-- FantaTournament.Domain/
|   |   |-- FantaTournament.Domain.csproj                     # Domain Layer Project
|   |-- FantaTournament.Infrastructure/
|   |   |-- FantaTournament.Infrastructure.csproj             # Infrastructure Layer Project
|   |-- FantaTournament.Api/
|   |   |-- FantaTournament.Api.csproj                        # REST API Layer Project
|   |-- UI                                                   # Folder to store UI
|   |-- Core                                                  # Folder to store shared libraries / cross topics
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

- Domain: it contains the business logic (aka: the domain library), the application layer and infrastructure library for the specific-domain-related concepts, like persistence.
- Infrastructure: it contains all needed libraries related to cross topic and not to business logic.
- WebApi: it contains the source code fro REST api to provide a port for the domain
- Clients: if needed, it contains the clients for Web Api.
