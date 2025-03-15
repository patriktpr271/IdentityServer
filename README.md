# IdentityServer Authentication System

A robust **JWT-based authentication system** built with **ASP.NET Core**. This project implements secure user authentication, password hashing, and token generation using industry-standard practices. Originally designed for a friend's web application, this system is now part of my professional portfolio.

## ✨ Features

- **JWT Authentication**: Secure user authentication with JSON Web Tokens.
- **User Management**: Includes user registration, login, and password hashing.
- **Custom Token Provider**: Generates JWTs with configurable claims and expiration.
- **Secure Password Hashing**: Implements a secure hashing mechanism for user passwords.
- **Validation Layer**: Ensures robust input validation for user data.
- **Configurable via appsettings.json**: Flexible JWT settings (e.g., secret, issuer, audience).

## 🛠️ Technologies Used

- **.NET 9**
- **ASP.NET Core**
- **JWT (JSON Web Tokens)**
- **Dependency Injection**
- **Entity Framework Core**

### API Endpoints

- **POST /api/auth/register**: Register a new user
- **POST /api/auth/login**: Authenticate and receive a JWT
- **POST /api/users**: Create a new user
- **GET: /api/users{id}:** Get the data of one user
- **GET: /api/users/email/{email}:** Get the data of one user based on the email
- **PUT: /api/users{id}:** Updates the data of one user
- **DELETE: /api/users{id}:** Deletes the data of one user

## 🔍 Example JWT Response

```json
{
  "token": "your.jwt.token",
  "user": {
    "id": "1234",
    "email": "user@example.com",
    "name": "John Doe"
  }
}
```

## 🧰 Posibble ways to Extend the Project

- Add role-based authorization.
- Implement user account recovery flows (password reset).

