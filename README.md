# Sistema de Gestión de Comandas

Sistema backend robusto desarrollado con **Clean Architecture** y **CQRS**, diseñado para la gestión eficiente de pedidos.

## 🏗️ Arquitectura
El proyecto se divide en las siguientes capas:
- **Domain:** Entidades centrales y reglas de negocio.
- **Application:** Casos de uso implementados con **MediatR** (CQRS).
- **Infrastructure:** Persistencia de datos utilizando **Entity Framework Core** y **PostgreSQL**.
- **API:** Web API construida en .NET 9, documentada con **Swagger/OpenAPI**.

## 🚀 Cómo empezar
1. Asegúrate de tener instalado el SDK de .NET 9.
2. Configura tu cadena de conexión a PostgreSQL en `appsettings.json`.
3. Ejecuta la API desde la carpeta `TShirt.Comandas.API`:
   ```bash
   dotnet run