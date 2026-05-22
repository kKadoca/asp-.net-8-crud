# API - Agendamento de Barbearia

Desafio Final do Bootcamp de Arquitetura de Software:
Uma API REST de agendamento para Barbearia em ASP.NET 8 (MVC)

---

## Stack

| Layer | Tech |
|-------|-----------|
| Runtime | .NET 8 |
| Framework | ASP.NET Core Web API |
| ORM | Entity Framework Core 8 |
| Database | SQLite |
| Docs | Swagger |

---

## Arquitetura & Padrões

### Padrão Arquitetural
- **MVC** — Controllers lidam com HTTP, Services lidam com regras de negócio, Postman funciona como a View.
- **Repository Pattern** — Acesso a dados por interface, desacoplado do Entity Framework.
- **Service Layer (Facade)** — Acesso simplificado aos repositórios + validações; Controllers desacopladas.
- **DTO Pattern** — Separaçãod e interfaces de req/res do Domínio.
- **Soft Delete** — Nenhum registro é escluído de verdade; `IsDeleted` flag + filtro global no Entity Framework.

### Princípios SOLID
- **Single Responsibility** — `CustomersController` lida com HTTP, `CustomerService` valida e orquestra, `CustomerRepository` chega no banco de dados, `ExceptionHandlingMiddleware` lida e formata erros.
- **Interface Segregation** — Cada Service e Repository tem sua própria Interface; `ISoftDeletable` é um exemplo mínimo de contrato entre Entidades que permitem soft-delete.
- **Dependency Inversion** — Nenhuma classe depende de implementações concretas: `Controller → IService → IRepository`.

### GoF Design Patterns
- **Dependency Injection** — Nativo do .NET

- **State Pattern** (`src/States/`) — State para o status do agendamento, implementando regras de transição de estados; Determinando estados terminais e possíveis transições. A camada Service nunca valida `if (status == X)` diretamente, mas sim pergunta ao State.

  ```
  AppointmentState (abstract)
    ├── ScheduledState  → Transiciona para: Completed, Cancelled, NoShow
    ├── CompletedState  → Terminal (não transiciona)
    ├── CancelledState  → Terminal (não transiciona)
    └── NoShowState     → Terminal (não transiciona)
  ```

- **Template Method** (`src/Repositories/BaseRepository.cs`) — `BaseRepository` define o esqueleto de todas as operações de um CRUD, para que cada implementação concreta referencie o seu `DbSet`.

	```
	BaseRepository<T> (abstract)
		├── DbSet              → hook
		├── GetAllAsync()      → virtual
		├── GetCountAsync()    → template
		├── AddAsync()         → template
		├── UpdateAsync()      → template
		└── DeleteAsync()      → template
	```

---

## Estrutura de Pastas

```
src/
├── Controllers/          # Camada HTTP — recebe requests, chama as Services, retorna JSON
├── Models/               # Entidades do domínio + ISoftDeletable interface
├── DTOs/                 # Request/response interfaces (Create, Update, Response por Entity)
├── Services/             # Lógica de Negócio
│   └── Interfaces/
├── Repositories/         # Acesso a dados Entity Framework Core
│   └── Interfaces/
├── Data/                 # AppDbContext + Migrations
├── States/               # State Pattern — AppointmentState + 4 states concretos
├── Middleware/           # Tratamento de exceções global
├── Exceptions/           # Implementações de Exceptions
├── Program.cs            # Entry point do App; Aqui fica a Injeção de Dependencias nativa
└── appsettings.json      # Conexão e logging
```

---

## Entidades

| Entity | Key Fields |
|--------|-----------|
| `Customer` | Name, Email (unico), Phone |
| `Professional` | Name, Phone (unico) |
| `PrestationOfService` | Name (unico), Description, Price, DurationMinutes |
| `Appointment` | DateTime, Status, FK → Customer, Professional, PrestationOfService |

**Appointment status:** `Scheduled` · `Completed` · `Cancelled` · `NoShow`

- [Entity Relationship Diagram](docs/entity-relationship-diagram.md)
- [Component Diagram](docs/component-diagram.md)
- [State Diagram](docs/state-diagram.md)
- [Sequence Diagram](docs/sequence-diagram.md)

---

## Setup

**Pre-requisitos:** .NET 8 SDK

```bash
# 1
cd src

# 2
dotnet restore

# 3
dotnet ef database update

# 4
dotnet run
```

API default: `http://localhost:5152` (HTTP) ou `https://localhost:7042` (HTTPS).  
Swagger: `http://localhost:5152/swagger`

---

## Endpoints

Todas as rotas pre-fixadas com `/api`.

### Customers `/api/customers`

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/customers` | List all |
| GET | `/api/customers/{id}` | Get by ID |
| GET | `/api/customers/count` | Total count |
| GET | `/api/customers/search?name=` | Search by name |
| POST | `/api/customers` | Create |
| PUT | `/api/customers/{id}` | Update |
| DELETE | `/api/customers/{id}` | Soft delete |

### Professionals `/api/professionals`

Mesmo formato do Customers — troque `customers` → `professionals`.

### Prestations of Service `/api/prestationsofservice`

Mesmo formato do Customers — troque `customers` → `prestationsofservice`.

### Appointments `/api/appointments`

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/appointments` | List all |
| GET | `/api/appointments/{id}` | Get by ID |
| GET | `/api/appointments/count` | Total count |
| GET | `/api/appointments/search?status=` | Filter by status |
| POST | `/api/appointments` | Create |
| PUT | `/api/appointments/{id}` | Update |
| DELETE | `/api/appointments/{id}` | Soft delete |