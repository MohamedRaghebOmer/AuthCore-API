# 🚀 AuthCore API

AuthCore API is a minimal, focused RESTful authentication service built on ASP.NET Core (.NET 10). It demonstrates practical API design with JWT access tokens, refresh tokens, and secure logout behavior. This project is intended for hands-on practice and to showcase understanding of REST principles (resources, statelessness, proper status codes, and clear request/response contracts). 

## ✅ Key Features

- JWT-based authentication with access/refresh token flow
- Secure refresh token rotation and revocation
- BCrypt password hashing
- Swagger/OpenAPI documentation
- Clean DTO-based request/response contracts

## 🧰 Tech Stack

- ASP.NET Core (.NET 10)
- JWT Bearer authentication
- Swashbuckle (Swagger)
- BCrypt.Net-Next

## 🗂️ Project Structure

- `Controllers/` — API endpoints (AuthController)
- `Services/` — authentication logic (token creation, refresh, logout)
- `Models/` — domain model and DTOs
- `Data/` — in-memory users store for demo purposes
- `Configurations/` — Swagger and authentication configuration

## 🔌 API Endpoints

Base route: `/api/auth`

### POST `/login`

Authenticates a user and returns access/refresh tokens.

**Request**
```json
{
  "username": "mohamedragheb",
  "password": "1234"
}
```

**Responses**
- `200 OK` — tokens issued
- `401 Unauthorized` — invalid credentials

### POST `/refresh`

Rotates refresh token and returns a new access/refresh token pair.

**Request**
```json
{
  "username": "mohamedragheb",
  "refreshToken": "<refresh-token>"
}
```

**Responses**
- `200 OK` — tokens issued
- `401 Unauthorized` — invalid or expired refresh token

### POST `/logout`

Revokes the current user refresh token.

**Headers**
```
Authorization: Bearer <access-token>
```

**Responses**
- `204 No Content` — logout completed
- `401 Unauthorized` — invalid access token

## 👥 Demo Users

The API ships with an in-memory user list in `Data/UsersData.cs`.

- `mohamedragheb / 1234` (Admin)
- `ahmedali / 1234` (User)
- `sarahmohamed / 1234` (Manager)

## ▶️ Running the API

1. Open the solution in Visual Studio.
2. Run the project (HTTPS profile).
3. In Development, Swagger UI is available at:
   - `https://localhost:<port>/swagger`

## 🔐 Token Behavior

- Access token lifetime: 15 minutes
- Refresh token lifetime: 7 days
- Refresh tokens are rotated on use and revoked on logout

## 📌 Notes on REST Practice

- Clear resource-oriented routes (`/api/auth/login`, `/api/auth/refresh`, `/api/auth/logout`)
- Stateless requests with tokens sent via headers or body
- Proper HTTP status codes for success and error states
- DTOs enforce consistent, explicit request/response contracts
