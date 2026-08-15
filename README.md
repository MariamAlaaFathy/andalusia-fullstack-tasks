# Tasks API (Pagination, Filtering & Sorting)

The Tasks API `GET` endpoint now supports pagination, filtering, and sorting
in a single composable query.

## Models

| Model                | Purpose                                          |
|-----------------------|---------------------------------------------------|
| `PaginationParams`     | `page`, `pageSize` (capped at 100)                |
| `TaskFilterParams`     | Extends pagination with `search`, `isCompleted`, `status`, `createdAfter`, `createdBefore`, `sortBy`, `order` |
| `PagedResult<T>`       | Wraps `data` with `totalCount`, `totalPages`, `hasNextPage`, `hasPreviousPage` |

## Query parameters

| Param           | Type     | Notes                                      |
|------------------|----------|---------------------------------------------|
| `search`         | string   | Matches task title (case-insensitive)       |
| `isCompleted`    | bool     | Filters by completion state                 |
| `status`         | string   | Filters by status                           |
| `createdAfter`   | datetime | Bonus - only tasks created after this date  |
| `createdBefore`  | datetime | Bonus - only tasks created before this date  |
| `sortBy`         | string   | `title`, `isCompleted`, `status`, `dueDate`, `createdAt`. Unknown values fall back to `createdAt` instead of crashing |
| `order`          | string   | `asc` (default) or `desc`                   |
| `page`           | int      | Defaults to 1                               |
| `pageSize`       | int      | Defaults to 10, capped at 100 regardless of what's sent |

## Example

```
GET /api/v2/tasks?search=meeting&isCompleted=false&page=1&pageSize=5&sortBy=title
```

## Response shape

Every response includes the paging metadata alongside the data, even on an
empty result set:

```json
{
  "data": [ /* tasks */ ],
  "page": 1,
  "pageSize": 5,
  "totalCount": 12,
  "totalPages": 3,
  "hasNextPage": true,
  "hasPreviousPage": false
}
```

## Postman collection

Example requests (valid requests, edge cases, and invalid requests) are in `TaskSixPostman.json`.
