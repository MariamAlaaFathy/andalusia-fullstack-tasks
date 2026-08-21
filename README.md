# Task Manager API (EF Core + Database)

The Task Manager API now persists data in a real database via Entity
Framework Core, replacing the in-memory storage used in earlier tasks.
Everything built previously - layered Controller/Service/Repository
architecture, global exception handling, URL-based API versioning, and
composable pagination/filtering/sorting - still sits on top of this data
layer unchanged.

## What's new in this task

- `AppDbContext` with `DbSet<TaskItem>` and `DbSet<User>`
- Fluent API configuration for both entities: required fields, max
  lengths on every string column, default values, and the foreign key
  relationship between `Tasks` and `Users`
- The connection string lives in `appsettings.json`, which is
  listed in `.gitignore` and not committed to the repository
- **Bonus**: `Tasks` and `Users`'s configurations are extracted into their own
  `TasksConfiguration : IEntityTypeConfiguration<TaskItem>` and `UsersConfiguration : IEntityTypeConfiguration<TaskItem>` classes and
  registered via `modelBuilder.ApplyConfiguration(new TasksConfiguration())` and `modelBuilder.ApplyConfiguration(new UsersConfiguration())`,
  keeping `AppDbContext.OnModelCreating` clean as the model grows

## Entities

| Entity     | Relationship                          |
|------------|-----------------------------------------|
| `Users`     | One user can have many tasks            |
| `Tasks` | Belongs to a `User` via a foreign key   |

## Data persistence

Unlike the earlier in-memory version, data now survives restarts - it's
stored in the configured database rather than reset on every run.

## Configuration

`appsettings.json` (gitignored) needs a `ConnectionStrings`
section with your local connection string before running the app.

## Migrations
 
Schema changes are tracked with EF Core migrations, generated from the
`AppDbContext` model (including the Fluent API configuration).
 
```
dotnet tool install --global dotnet-ef   # one-time, if not already installed
dotnet ef migrations add InitialCreate
dotnet ef database update
```
 
Each time the model changes (a new field, a new entity, a config change in
`OnModelCreating`, `TasksConfiguration`, or `UsersConfiguration`), a new migration is added the
same way with a descriptive name:
 
```
dotnet ef migrations add AddUserEmailIndex
dotnet ef database update
```
