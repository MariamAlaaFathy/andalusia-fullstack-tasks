# Task Management API with Global Exception Handling

A RESTful Web API built with **.NET Core** for managing tasks, featuring repository-service architecture, custom exception handling, and RFC 7807 compliant `ProblemDetails` error responses.

---

## Features

* **Task CRUD Operations:** Retrieve, create, update, and delete tasks.
* **Clean Architecture:** Divided into Controller, Service, and Repository layers.
* **Custom Exceptions:** Domain-specific exceptions for business rules (e.g., past due dates, duplicate titles, invalid IDs).
* **Global Error Handling Middleware:** Intercepts uncaught exceptions and formats them into standard `ProblemDetails` (`application/problem+json`) responses.
* **No Try-Catch Bloat:** Controllers stay lean while middleware handles application-wide exceptions.

---

## Data Model (`Tasks`)

| Field | Type | Description |
| --- | --- | --- |
| `Id` | `int` | Unique identifier for the task. |
| `Title` | `string` | Title of the task (must be unique). |
| `DueDate` | `DateTime` | Scheduled completion date (cannot be in the past). |
| `IsCompleted` | `bool` | Indicates whether the task is finished. |

---

## API Endpoints

Base Route: `/api/tasks`

| Method | Endpoint | Description | Expected Status |
| --- | --- | --- | --- |
| `GET` | `/api/tasks` | Get all tasks | `200 OK`<br> |
| `GET` | `/api/tasks/{id}` | Get task by ID | `200 OK`<br> |
| `POST` | `/api/tasks` | Create a new task | `201 Created`<br> |
| `PUT` | `/api/tasks/{id}` | Update an existing task | `200 OK`<br> |
| `DELETE` | `/api/tasks/{id}` | Delete a task by ID | `204 No Content`<br> |

---

## Exception Handling & HTTP Error Statuses

Custom exceptions are intercepted by `GlobalExceptionMiddleware` and mapped to HTTP status codes:

| Exception | HTTP Status Code | Reason / Trigger |
| --- | --- | --- |
| `InvalidIdException` | `400 Bad Request` | Provided task ID is less than or equal to `0`. |
| `NotFoundException` | `404 Not Found` | Requested task ID does not exist. |
| `ConflictException` | `409 Conflict` | Creating/updating a task with a duplicate title. |
| `DueDateInPastException` | `422 Unprocessable Entity` | Setting a `DueDate` earlier than the current time. |
| `Exception` (Unhandled) | `500 Internal Server Error` | Generic fallback response with hidden stack trace. |

---

## Postman Collection

A pre-configured Postman collection is included in the project under `Tasks Requests.postman_collection.json`.

### Included Test Cases:

* **Valid Requests:**
  * `GET` all tasks
  * `GET` task by ID
  * `POST` new task
  * `PUT` update task
  * `DELETE` task.


* **Invalid Requests:**
  *  `GET /api/tasks/999` $\rightarrow$ Triggers `404 Not Found`.
  * `POST /api/tasks` (Duplicate Title) $\rightarrow$ Triggers `409 Conflict`.
  * `POST /api/tasks` (Past Due Date) $\rightarrow$ Triggers `422 Unprocessable Entity`.
