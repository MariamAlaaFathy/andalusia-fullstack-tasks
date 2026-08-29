# Swagger Documentation & Validation

Full Swagger/OpenAPI documentation added to the Tasks and Users endpoints,
plus FluentValidation coverage on every request DTO/entity accepted by
those endpoints.

## Swagger documentation

Every action on `TasksController` (v2) and `UserController` has:
- An `/// <summary>` describing what it does
- `/// <response code="...">` tags describing every possible outcome
- Matching `[ProducesResponseType]` attributes so Swagger UI shows the
  correct response type/schema per status code (200/201/204/400/404/409)

## Validation

| Validator                        | Applies to            | Rules                                                                                                              |
|----------------------------------|-----------------------|--------------------------------------------------------------------------------------------------------------------|
| `CreateTaskRequestValidator`     | `CreateTaskRequest`   | Title required, max 200 chars, no HTML tags; due date required and must be in the future when set; UserId required |
| `UpdateTaskRequestValidator`     | `UpdateTaskRequest`   | Same rules as create except for UserId                                                                             |
