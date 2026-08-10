# ProductsApi

A simple ASP.NET Core Web API for managing products, using in-memory storage.
Built with a layered architecture: **Controller → Service → Repository**.

## Endpoints

| Method | Route                | Description                    | Success | Failure   |
|--------|-----------------------|----------------------------------|---------|-----------|
| GET    | `/api/products`       | Get all products                 | 200     | —         |
| GET    | `/api/products/{id}`  | Get a single product             | 200     | 400 / 404 |
| POST   | `/api/products`       | Create a product                 | 201     | —         |
| PUT    | `/api/products/{id}`  | Replace a product entirely       | 200     | 400 / 404 |
| DELETE | `/api/products/{id}`  | Delete a product                 | 204     | 400 / 404 |
| PATCH  | `/api/products/{id}`  | Update only the `Name` field     | 200     | 400 / 404 |

- No endpoint returns `200` when an error occurs.
- Test requests for all endpoints are in `TaskThreePostman.json`.

## Data persistence

Storage is in-memory only, so data resets when the app restarts.
