# Tasks API

An ASP.NET Core Web API for managing tasks, using in-memory storage.
Built with a layered architecture: **Controller → Service → Repository**,
plus a `GlobalExceptionMiddleware` that returns structured `ProblemDetails`
responses for all errors.

## Endpoints

| Method | Route             | Description         | Success | Failure         |
|--------|-------------------|----------------------|---------|-----------------|
| GET    | `/api/tasks`      | Get all tasks        | 200     | —               |
| GET    | `/api/tasks/{id}` | Get a single task    | 200     | 400 / 404       |
| POST   | `/api/tasks`      | Create a task        | 201     | 409 / 422       |
| PUT    | `/api/tasks/{id}` | Replace a task       | 200     | 400 / 404 / 409 / 422 |
| DELETE | `/api/tasks/{id}` | Delete a task        | 204     | 400 / 404       |

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

## Data persistence

Storage is in-memory only, so data resets when the app restarts.

## Postman collection

Test requests for valid and invalid cases are in `TaskFourPostman.json`.
