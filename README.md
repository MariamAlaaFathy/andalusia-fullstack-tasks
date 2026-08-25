# Cinema Ticket Booking API

A RESTful ASP.NET Core Web API for a cinema ticket booking system, built
with a layered architecture (Controller → Service → Repository), EF Core,
DTOs throughout, global exception handling, URL-based API versioning, and
composable pagination/filtering/sorting.

## Features

- **CRUD** for movies, auditoriums, showtimes, customers, and bookings
- **API versioning** on movies — v1 (deprecated, compact shape) and v2
  (current, detailed shape) served side by side
- **Pagination, filtering & sorting** on movies (search, genre, sort by
  name/release date) and bookings (by customer, showtime, status)
- **Global exception handling** middleware returning `ProblemDetails` for
  every error, in `application/problem+json`
- **DTOs + AutoMapper** — entities never cross the API boundary
- **Business rules enforced server-side**: no duplicate movie titles, no
  scheduling unavailable movies/auditoriums, no deleting a movie/auditorium/
  showtime with dependent records, booking status only changes through
  dedicated confirm/cancel endpoints
- **Guest checkout** — customers are identified by name + email, no
  authentication required
- **Swagger / OpenAPI** documentation with full XML doc comments and
  per-status-code response types on every endpoint

## Tech stack

- ASP.NET Core Web API (.NET)
- Entity Framework Core (SQL Server)
- AutoMapper
- Asp.Versioning.Mvc
- Swashbuckle (Swagger)

## Project structure

```
CinemaTicketBookingProject/
├── Controllers/
│   ├── V1/MoviesV1Controller.cs
│   ├── V2/MoviesV2Controller.cs
│   ├── AuditoriumsController.cs
│   ├── ShowTimesController.cs
│   ├── CustomerController.cs
│   └── BookingsController.cs
├── Services/            (+ Interfaces/)
├── Repositories/         (+ Interfaces/)   # only classes that touch AppDbContext
├── Model/                # Movie, Auditorium, ShowTime, Customer, Booking
├── DTOs/
├── Enums/
│   └── BookingStatus.cs  # Pending | Confirmed | Cancelled
├── Exceptions/
├── Middleware/
│   └── GlobalExceptionMiddleware.cs
├── Data/
│   └── AppDbContext.cs
└── Mapping/
    └── MappingProfile.cs
```

## Entity relationships

- One **Movie** → many **ShowTimes**
- One **Auditorium** → many **ShowTimes**
- Each **ShowTime** belongs to one Movie and one Auditorium
- One **Customer** → many **Bookings**
- Each **Booking** belongs to one Customer and one ShowTime

## API versioning

| Version | Route            | Fields                                                     |
|---------|-------------------|---------------------------------------------------------------|
| v1      | `/api/v1/movies` | `id`, `name`, `availableInCinema` — deprecated, read-only     |
| v2      | `/api/v2/movies` | `id`, `name`, `genre`, `releaseDate`, `availableInCinema` — full CRUD |

## Error handling

All errors return `application/problem+json` with a real `ProblemDetails`
body:

| Exception                    | Status | Meaning                                                    |
|--------------------------------|--------|----------------------------------------------------------------|
| `MovieNotFoundException`       | 404    | Movie doesn't exist                                            |
| `AuditoriumNotFoundException`  | 404    | Auditorium doesn't exist                                       |
| `ShowTimeNotFoundException`    | 404    | Showtime doesn't exist                                         |
| `CustomerNotFoundException`    | 404    | Customer doesn't exist                                         |
| `BookingNotFoundException`     | 404    | Booking doesn't exist                                          |
| `MovieAlreadyExistsException`  | 409    | Duplicate movie title                                          |
|`CustomerAlreadyExistsException`| 409    | Duplicate customer email                                       |
|`EntityShouldNotBeDeletedException`| 409    | Delete blocked by dependent records, or scheduling an unavailable movie/auditorium |
| `InvalidBookingException`      | 400    | Booking a past showtime, or an invalid status transition        |
| Validation failure             | 400    | A request DTO failed its data annotations                      |
| Unhandled exception            | 500    | Generic message, no stack trace leaked                         |

## Testing

- **Swagger UI** — try every endpoint interactively, grouped by API version
- **Postman** — see the included collection for example requests covering
  both valid flows and edge cases (not found, conflicts, validation errors)
