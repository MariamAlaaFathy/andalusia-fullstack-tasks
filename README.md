# Task Manager API (Repository Pattern & DTOs)

The Task Manager API is refactored so `TaskService` never touches
`AppDbContext` directly - all data access goes through `ITaskRepository`,
and all controller responses go through DTOs instead of raw entities.

## What's new in this task

- `ITaskRepository` / `TaskRepository` - owns all EF Core / `AppDbContext`
  access, registered as **Scoped** in `Program.cs`
- `TasksDTO`, `CreateTaskRequest`, `UpdateTaskRequest` - the only shapes
  that cross the API boundary
- `MappingProfile` (AutoMapper) - the single place entity ↔ DTO field
  assignments are defined, including setting `CreatedAt` via `ForMember`
  during the create mapping
- `TaskService` now depends on `ITaskRepository` and `IMapper` only - zero
  references to `AppDbContext`
- **Bonus**: `TaskSummaryDTO` (`Id`, `Title`, `IsCompleted` only), mapped in
  `MappingProfile` and used by `GET /api/tasks` so the list endpoint is
  lighter than the single-item response

## Request/response shapes

| Endpoint             | Accepts             | Returns           |
|------------------------|----------------------|----------------------|
| `GET /api/tasks`       | —                    | `TaskSummaryDTO[]`   |
| `GET /api/tasks/{id}`  | —                    | `TasksDTO`           |
| `POST /api/tasks`      | `CreateTaskRequest`  | `TasksDTO`           |
| `PUT /api/tasks/{id}`  | `UpdateTaskRequest`  | `TasksDTO`           |
| `DELETE /api/tasks/{id}` | —                  | —                    |

`CreateTaskRequest` has no `id` or `createdAt` field, so sending either in
the body has no effect - both are set server-side (`Id` by the database,
`CreatedAt` via `ForMember` in `MappingProfile`).

## Layering (cumulative across all tasks)

```
Controller  ->  Service (business rules, mapping via IMapper) ->  ITaskRepository (all AppDbContext / EF Core access)
```

No entity navigation properties or sensitive fields ever reach the
response body - `MappingProfile` is the only place that decides what an
entity's fields become in a DTO.
