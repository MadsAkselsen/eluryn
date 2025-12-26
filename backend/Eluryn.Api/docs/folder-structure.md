## Overview

The chosen architecture and folder structure for this Eluryn is Modular Monolith using Vertical Slice.

For now, there will be a single shared DB. Each table will belong to a module and only the module owner can access the table. 
This means each module will have it's own DB migrations.

Inspiration: 
- https://antondevtips.com/blog/building-a-modular-monolith-with-vertical-slice-architecture-in-dotnet?utm_source=reddit&utm_medium=social&utm_campaign=02-05-2025
- Best one: https://dometrain.com/blog/getting-started-with-modular-monoliths-in-dotnet/#section-9-when-to-extract-a-module

##

backend
- Eluryn.API
  - Program.cs
  - AppSettings.json
  - Extensions/
  - Shared/
    - Errors
    - Time
    - Results
    - Abstractions

  - modules/
    - Users
      - Users.Api
        - Endpoints
          - LookupUserEndpoint.cs
        - UserApiModule.cs
      - Users.Application
        - LookupUsers/
          - Query.cs
          - Handler.cs
          - Response.cs
        - CreateUser
      - Users.Infrastructure
        - Persistance
          - UsersDbContext.cs
          - Configurations
          - Migrations/
            - V1__Init_Users.sql
            - V2__add__avatar.sql
        - PublicApi
          - UsersPublicApi.cs
        - UsersInfrastructureModule.cs
      - Users.Contract
        - IUserPublicApi.cs
        - UserSummaryDto.cs
    - Chat
    - Pomodoro
    - Presence

  - Infrastructure
    - LocalDev/
      - docker-compose.yml
    - Prd
      - docker-compose.yml
    - 
