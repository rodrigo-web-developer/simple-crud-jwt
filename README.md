# Projeto API RESTful com ASP.NET Core + OData + Autenticação JWT

Este projeto é uma API RESTful desenvolvida em ASP.NET Core 8 com:
- Autenticação via JWT
- Consulta OData para entidades
- Padrão MVC + Repository + Service
- Persistência em PostgreSQL
- Docker-ready com `docker-compose`
- Dados de exemplo com Seed automático

---

## 🚀 Como rodar

### 🔸 1. Rodar com Docker

> Requer: Docker + Docker Compose

```bash
docker-compose up --build
```

A API estará acessível em:

```
http://localhost:5000/swagger
```

Banco de dados PostgreSQL disponível em:

```
localhost:5432
user: postgres
pass: postgres
db: storedbapp
```

---

### 🔸 2. Rodar via .NET CLI (sem Docker)

> Requisitos: .NET 8 SDK + PostgreSQL (configure a connection string em  `appsettings.json`)

```bash
dotnet restore
dotnet ef database update
dotnet run
```

Acesse:
```
http://localhost:5000/swagger
```

---

## 🧪 Credenciais de Teste

### Admin
```json
{
  "email": "admin@admin.com",
  "senha": "admin@123"
}
```

### Usuários Parceiros
- `partner1@teste.com` – Parceiro A  
- Senha: `partner1@password`
- `partner1@teste.com` – Parceiro B  
- Senha: `partner2@password`

---

## 🔐 Autenticação JWT

1. Faça login via:
```
POST /api/auth/login
```

2. Copie o token JWT retornado.
3. No Swagger, clique em **Authorize** e cole:
```
Bearer <token copiado>
```

---

## 📦 Endpoints

- `/odata/Clientes`
- `/odata/Produtos`
- `/odata/Pedidos`
- `/odata/Parceiros`
- `/odata/Usuarios`

Todos com suporte a:
- `$filter`
- `$orderby`
- `$select`
- `$top`
- `$count`

---

## 📂 Estrutura de Projeto

```
/Controllers
/Services
/Repositories
/Models
/Data
/Auth
/Middleware
```

---

## 🧰 Ferramentas e Bibliotecas

- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- OData
- Swagger (Swashbuckle)
- JWT Bearer Auth
- BCrypt.Net

---

## 🔐 Filtro baseado em autenticação
Ao invocar uma API de consulta OData, o usuário admin tem acesso a todos os dados de produtos e pedidos, enquanto os usuários de parceiros conseguem apenas enxergar os seus próprios produtos ou pedidos.
