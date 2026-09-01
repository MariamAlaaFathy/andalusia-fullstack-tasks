# JWT Authentication

Full JWT authentication added to the Task Manager API. `TasksController`
is now protected end-to-end - every task query, create, update, and
delete is scoped to whichever user the token belongs to.

## What's new in this task

- `POST /api/users` - hashes the password with BCrypt, rejects
  duplicate emails with `409`
- `POST /api/users/login` - verifies the password hash and returns a JWT;
  wrong email or wrong password both return the same `401 Invalid
  credentials.` (never reveals which one was wrong)
- `UsersService.GenerateToken` - embeds `UserId`, `Email`, and `Role` as
  claims (`sub`/`ClaimTypes.NameIdentifier`, `ClaimTypes.Email`,
  `ClaimTypes.Role`)
- `Program.cs` - `AddAuthentication().AddJwtBearer()` with all four
  validation parameters on (issuer, audience, signing key, lifetime), and
  `UseAuthentication()` placed before `UseAuthorization()` before
  `MapControllers()`
- `[Authorize]` on the whole `TasksController` - every action reads the
  caller's id from the token's claims (`CurrentUserId` property), never
  from the route, query string, or request body
- **Bonus**: Swagger's "Authorize" button, via `AddSecurityDefinition` +
  `AddSecurityRequirement` in `Program.cs` - paste a token once and every
  request in Swagger UI carries it automatically

## Why task creation is safe from spoofing

`CreateTaskRequest`/`UpdateTaskRequest` no longer have a `UserId` field at
all - it's set server-side from the JWT's claims inside
`TasksController.CurrentUserId`, then passed explicitly into every
`ITasksService` method. There's no field in the request body a client
could set to claim someone else's user id.

## Auth flow

| Step | Endpoint | Result |
|------|-----------|--------|
| 1 | `POST /api/users` | `200` on success, `409` if the email is taken |
| 2 | `POST /api/users/login` | `200` + JWT on success, `401` on wrong email/password |
| 3 | `GET /api/tasks` (no token) | `401` |
| 4 | `GET /api/tasks` (valid token) | `200` - only that user's tasks |
| 5 | `GET /api/tasks` (tampered token) | `401` - signature no longer validates |
