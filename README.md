# MegaStore Global — REST API

> Backend solution for MegaStore Global's data modernization project, built by LogiTech Solutions.
> Migrates a legacy flat-file (Excel/CSV) system into a normalized relational database exposed through a RESTful API.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Runtime | Node.js 18+ (ESM) |
| Framework | Express.js |
| Database | PostgreSQL |
| Dev Server | Nodemon |
| HTTP Client (testing) | Postman |

---

## Project Structure

```
megastore/
├── server.js                        # Bootstrap — validates DB then starts server
├── .env                             # Environment variables (never commit this)
├── package.json
└── src/
    ├── app.js                       # Express app: middlewares, routes, error handler
    ├── config/
    │   ├── env.js                   # Centralized env variable access
    │   └── postgres.js              # Connection pool + ping check
    ├── routes/
    │   ├── productos.route.js       # CRUD route definitions
    │   └── bi.route.js              # Business intelligence route definitions
    ├── controllers/
    │   ├── productos.controller.js  # Extracts req/res, delegates to service
    │   └── bi.controller.js
    ├── services/
    │   ├── productos.service.js     # Business logic + SQL queries
    │   └── bi.service.js            # BI queries + param validation
    └── middlewares/
        └── errorHandler.js          # Global error formatter
```

---

## Architecture & Design Decisions

### Why this layered structure?

The project follows a **3-layer architecture** (without a separate repository layer):

```
Request → Route → Controller → Service (+ SQL) → PostgreSQL → Response
```

| Layer | Responsibility |
|---|---|
| **Route** | Maps HTTP method + URL to a controller function |
| **Controller** | Reads `req`, calls service, writes `res`. Zero business logic |
| **Service** | Validates inputs, runs SQL queries via `pool`, throws typed errors |
| **Middleware** | Catches all thrown errors and formats them into consistent JSON |

This separation makes the codebase easy to read, test, and extend — each file has one clear job.

### Database Design — Normalization (SQL)

The raw CSV source violated multiple normal forms. The normalization process:

- **1NF** — Each column holds a single atomic value. No repeating groups.
- **2NF** — All non-key attributes depend on the full primary key. Separated `categorias`, `proveedores`, `productos`, `clientes` into their own tables.
- **3NF** — Removed transitive dependencies. `categoria_nombre` no longer lives on `productos`; it belongs exclusively to `categorias`.

**Result: 7 clean, related tables:**

```
categorias          proveedores
     └── productos ──┘
              └── detalle_transaccion
clientes
     └── direcciones
     └── transacciones
              └── detalle_transaccion
```

**All tables enforce:**
- `UUID` primary keys (`gen_random_uuid()`)
- `NOT NULL` on required fields
- `UNIQUE` constraints where applicable (e.g. `sku`, `email`)
- Foreign key constraints with referential integrity

---

## Local Setup

### Prerequisites
- Node.js 18 or higher
- Access to a PostgreSQL instance
- npm

### 1. Clone the repository
```bash
git clone <repo-url>
cd megastore
```

### 2. Install dependencies
```bash
npm install
```

### 3. Configure environment variables
```bash
cp .env.example .env
```

Edit `.env` with your database credentials:
```env
APP_PORT=3000
DB_HOST=your_host
DB_PORT=5432
DB_NAME=your_database
DB_USER=your_user
DB_PWD=your_password
```

### 4. Start the development server
```bash
npm run dev
```

You should see:
```
🚀 Server running at http://localhost:3000
```

---

## API Reference

### Base URL
```
http://localhost:3000/api
```

---

### Products — CRUD

#### Create a product
```
POST /api/productos
```
**Body:**
```json
{
  "sku": "SKU-001",
  "nombre": "Laptop Pro 15",
  "categoria_id": "<uuid>",
  "proveedor_id": "<uuid>"
}
```

#### List all products
```
GET /api/productos
```

#### Get product by ID
```
GET /api/productos/:id
```

#### Update a product
```
PUT /api/productos/:id
```
**Body:** same fields as POST

#### Delete a product
```
DELETE /api/productos/:id
```

---

### Business Intelligence

#### Supplier analysis
Returns total items sold and total inventory value per supplier.
```
GET /api/bi/proveedores
```

#### Customer purchase history
Returns all transactions for a specific customer with product details.
```
GET /api/bi/clientes/:cliente_id/historial
```

#### Top products by category
Returns best-selling products within a category, ordered by revenue.
```
GET /api/bi/top-productos?categoria=Electronics
```

---

## Error Handling

All errors follow a consistent response format:

```json
{
  "ok": false,
  "error": "Descriptive error message"
}
```

| Status | Meaning |
|---|---|
| `400` | Missing or invalid fields |
| `404` | Resource not found |
| `500` | Unexpected server error |

---

## Environment Variables

| Variable | Description | Default |
|---|---|---|
| `APP_PORT` | Port the server listens on | `3000` |
| `DB_HOST` | PostgreSQL host | — |
| `DB_PORT` | PostgreSQL port | `5432` |
| `DB_NAME` | Database name | — |
| `DB_USER` | Database user | — |
| `DB_PWD` | Database password | — |

> ⚠️ **Never commit your `.env` file.** Make sure it is listed in `.gitignore`.

---

## Scripts

| Command | Description |
|---|---|
| `npm run dev` | Start server with hot-reload (nodemon) |
| `npm start` | Start server in production mode |

---

## Author

Developed as part of the **LogiTech Solutions** backend assessment for MegaStore Global's data modernization initiative.
# ComplejoDeportivo
# ComplejoDeportivo
# ComplejoDeportivo
