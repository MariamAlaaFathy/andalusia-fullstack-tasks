# Tasks API (Versioned)

URL-based API versioning added to the Tasks API using `Asp.Versioning.Mvc`.
v1 is deprecated in favor of v2, which introduces a breaking response shape.

## Versions

| Version | Route            | Response fields                          | Status |
|---------|------------------|-------------------------------------------|--------|
| v1      | `/api/v1/tasks`  | `id`, `title`, `isCompleted`              | Deprecated |
| v2      | `/api/v2/tasks`  | `id`, `title`, `status`, `dueDate`, `createdAt` | Current |

`status` is `"pending"`, `"in-progress"`, or `"completed"`.

## Endpoints (both versions)

| Method | Route                | Description      | Success | Failure               |
|--------|-----------------------|-------------------|---------|------------------------|
| GET    | `/api/v{n}/tasks`      | Get all tasks     | 200     | —                      |
| GET    | `/api/v{n}/tasks/{id}` | Get a single task | 200     | 400 / 404              |
| POST   | `/api/v{n}/tasks`      | Create a task     | 201     | 409 / 422              |
| PUT    | `/api/v{n}/tasks/{id}` | Replace a task    | 200     | 400 / 404 / 409 / 422  |
| DELETE | `/api/v{n}/tasks/{id}` | Delete a task     | 204     | 400 / 404              |

v1's request body doesn't need `dueDate` - a default is filled in automatically.

## Error handling

All errors are caught by `GlobalExceptionMiddleware` and returned as
`application/problem+json`:

| Exception               | Status | Meaning                          |
|--------------------------|--------|-----------------------------------|
| `InvalidIdException`     | 400    | Task ID is `0` or negative        |
| `NotFoundException`      | 404    | No task with that ID              |
| `ConflictException`      | 409    | A task with that title already exists |
| `DueDateInPastException` | 422    | Due date is earlier than today    |
| Unhandled exception      | 500    | Generic message, no stack trace   |

## Response headers

- `api-supported-versions: 1.0, 2.0` on every response
- `api-deprecated-versions: 1.0` on v1 responses
- `Sunset` header (one year out) on v1 responses, added by `SunsetHeaderMiddleware`

## Data persistence

Storage is in-memory only, so data resets when the app restarts.

## Postman collection

Test requests for valid v1 and v2 cases and invalid cases are in `TaskFivePostman.json`.
